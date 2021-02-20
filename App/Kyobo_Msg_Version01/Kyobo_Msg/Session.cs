using DSDO.COMMON.LIBRARY.Network;
using DSDO.COMMON.UTIL.Network;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

/*
 * DATE : 2020.08.10 
 * FORM_TITLE : 서버 SESSION 처리
 * AUTHOR : 교보정보통신 김현수
 * DESC : 서버에 연결된 세션을 관리한다.
 */

namespace Kyobo_Msg_Server
{
    enum MsgType : int
    {
        String = 0,
        File
    }

    public enum SState : int
    {
        State_Init = 0,
        State_Connect,
        State_CheckPC,
        //State_Login,
        State_UserInfoOK,

        //public const uint State_Wait = 0x12;        
    }

    class Session
    {
        private struct Header
        {
            public int datasSize;
            public int msgType;
            public int fileNameSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string fileName;
        }
        private static short HEADER_SIZE = 12 + 256;

        Header header;
        byte[] headerBuff;
        RecvBuff recvBuff;
        Socket socket;

        public string recvFileName { get; set; }

        public string userId { get; set; }


        SState sState;  //Server Session State
        public void SetSState(SState sState) { this.sState = sState; }


        public string GetIpAddress() { return ((IPEndPoint)(socket.RemoteEndPoint)).Address.ToString(); }

        public IPEndPoint EndPoint
        {
            get
            {
                if (socket != null && socket.Connected)
                    return (IPEndPoint)socket.RemoteEndPoint;

                return new IPEndPoint(IPAddress.None, 0);
            }
        }

        public delegate void DisconnectedEventHandler(Session sender);
        public event DisconnectedEventHandler Disconnected;
        public delegate void DataReceivedEventHandler(Session sender, RecvBuff e);
        public event DataReceivedEventHandler DataReceived;

        public delegate void DataSentEventHandler(Session sender, int sent);
        public event DataSentEventHandler DataSent;

        public Session(Socket s)
        {
            socket = s;
            userId = "";
            sState = SState.State_Init;
            
            headerBuff = new byte[HEADER_SIZE];
        }

        public void Close()
        {
            if (socket != null)
            {
                socket.Disconnect(false);
                socket.Close();
            }

            recvBuff.Dispose();
            socket = null;
            userId = "";
            sState = SState.State_Init;
          
            headerBuff = null;
            Disconnected = null;
            DataReceived = null;
        }

        public void ReceiveAsync()
        {
            header.datasSize = 0;
            header.msgType = 0;
            Array.Clear(headerBuff, 0, headerBuff.Length);

            socket.BeginReceive(headerBuff, 0, headerBuff.Length, SocketFlags.None, RecvHeaderCallBack, null);
        }

        public void RecvHeaderCallBack(IAsyncResult ar)
        {
            try
            {
                int rec = socket.EndReceive(ar);

                if (rec == 0)
                {
                    if (Disconnected != null)
                    {
                        Disconnected(this);
                        //Disconnected = null;  //Disconnected 함수에서 Close()를 호출 Close에서 null 진행
                        return;
                    }
                }
                if (rec != HEADER_SIZE)
                {
                    throw new Exception();
                }
            }
            catch (SocketException se)
            {
                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                        if (Disconnected != null)
                        {
                            Disconnected(this);
                            //Disconnected = null;  //Disconnected 함수에서 Close()를 호출 Close에서 null 진행
                            return;
                        }
                        break;
                }
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (NullReferenceException)
            {
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            header.datasSize = convertEndian(BitConverter.ToInt32(headerBuff, 0));
            header.msgType = BitConverter.ToInt32(headerBuff, 4);
            if (header.msgType == (int)MsgType.File)
            {

                header.fileNameSize = BitConverter.ToInt32(headerBuff, 8);
                header.fileName = System.Text.Encoding.Default.GetString(headerBuff, 12, header.fileNameSize);
                recvFileName = header.fileName;    // Utils.GetAddSecondFileName(header.fileName);
                //Console.Write("MsgType File");
                //System.Threading.Thread.Sleep(300);
            }

            recvBuff = new RecvBuff(header.datasSize);
            //Array.Clear(recvBuff.byteBuf, 0, recvBuff.byteBuf.Length);
            recvBuff.msgType = header.msgType;
            //if (header.msgType == (int)MsgType.File)
            //    recvBuff.fileName = header.fileName;

            socket.BeginReceive(recvBuff.byteBuf, 0, header.datasSize, SocketFlags.None, RecvDatasCallBack, null);
        }

        public void RecvDatasCallBack(IAsyncResult ar)
        {
            int rec = socket.EndReceive(ar);

            if (rec > 0)
            {
                //recvBuff.memoryStream.Write(recvBuff.byteBuf, 0, rec);

                recvBuff.toRecv -= rec;

                if (recvBuff.toRecv > 0)
                {
                    socket.BeginReceive(recvBuff.byteBuf, recvBuff.byteBuf.Length - recvBuff.toRecv, recvBuff.toRecv, SocketFlags.None, RecvDatasCallBack, null);
                    return;
                }

                if (DataReceived != null)
                {
                    recvBuff.memoryStream.Position = 0;

                    DataReceived(this, recvBuff);
                }

            }

            recvBuff.Dispose();

            ReceiveAsync();
        }

        public void Send(PacketDataReq request)
        {
            byte[] data = createProtocolPacket(request);
            Send(data, 0, data.Length);
            data = null;
        }

        public byte[] createProtocolPacket(PacketDataReq request)
        {
            string msg = JsonConvert.SerializeObject(request);
            byte[] contentsBuffer = System.Text.Encoding.UTF8.GetBytes(msg);

            Header header = new Header();
            header.datasSize = convertEndian(contentsBuffer.Length);
            header.msgType = (int)MsgType.String;
            //LogHelper.log("TcpParser createProtocolPacket", Convert.ToString(contentsBuffer.Length));

            byte[] headerBuffer = structureToByte(header);
            byte[] packet = new byte[contentsBuffer.Length + HEADER_SIZE];
            headerBuffer.CopyTo(packet, 0);
            contentsBuffer.CopyTo(packet, HEADER_SIZE);

            return packet;
        }

        public int convertEndian(int source)
        {
            byte[] data = BitConverter.GetBytes(source);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToInt32(data, 0);
        }

        private byte[] structureToByte(object obj)
        {
            int dataSize = System.Runtime.InteropServices.Marshal.SizeOf(obj);
            IntPtr buff = System.Runtime.InteropServices.Marshal.AllocHGlobal(dataSize);
            System.Runtime.InteropServices.Marshal.StructureToPtr(obj, buff, false);

            byte[] data = new byte[dataSize];
            System.Runtime.InteropServices.Marshal.Copy(buff, data, 0, dataSize);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(buff);
            return data;
        }

        public void Send(byte[] data, int index, int length)
        {
            //socket.BeginSend(BitConverter.GetBytes(length), 0, 4, SocketFlags.None, sendCallBack, null);
            //System.Threading.Thread.Sleep(10);
            socket.BeginSend(data, index, length, SocketFlags.None, sendCallBack, null);
        }

        public void sendCallBack(IAsyncResult ar)
        {
            try
            {
                int sent = socket.EndSend(ar);
                if (DataSent != null)
                {
                    DataSent(this, sent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("SEND ERROR\n{0}", ex.Message));
            }
        }
    }
}
