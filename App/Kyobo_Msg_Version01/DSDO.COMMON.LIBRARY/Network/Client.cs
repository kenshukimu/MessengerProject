using DSDO.COMMON.UTIL.Network;
using DSDO.COMMON.UTIL.SYSTEM;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

/*
* DATE : 2020.08.10 
* FORM_TITLE : 클라이언트 접속 클래스
* AUTHOR : 교보정보통신 김현수
* DESC : 클라이언트 접속 정보
*/
namespace DSDO.COMMON.LIBRARY.Network
{
    public class Client
    {
        Socket socket = null;
        
        //private TcpProtocolHelper tcpProtocolHelper = null;

        private struct Header
        {
            public int conetentsSize;
            public int msgType;
            public int fileNameSize;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string fileName;
        }
        private static short HEADER_SIZE = 12 + 256;

        private class StateObject
        {
            public Socket workSocket = null;
            public byte[] buffer = null;
            public int msgType = 0;
        }

        public bool Connected
        {
            get
            {
                if (socket != null)
                {
                    return socket.Connected;
                }
                return false;
            }
        }

        public delegate void OnConnectEventHandler(Client sender, bool connected);
        public event OnConnectEventHandler OnConnect;

        public delegate void OnSendEventHandler(Client sender, int sent);
        public event OnSendEventHandler OnSend;

        public delegate void OnDisconnectEventHandler(Client sender);
        public event OnDisconnectEventHandler OnDisconnect;

        public delegate void OnDisconnectByServerEventHandler(Client sender);
        public event OnDisconnectByServerEventHandler OnDisconnectByServer;

        public delegate void OnReceiveEventHandler(Client sender, int msgType, byte[] buff);
        public event OnReceiveEventHandler OnReceive;

        public Client()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ipAddress, int port)
        {
            try
            {
                if (socket == null)
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(IPAddress.Parse(ipAddress), port);

                if (OnConnect != null)
                {
                    OnConnect(this, Connected);
                }

                StartRecv(socket);
            }
            catch (SocketException se)
            {
                throw se;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (socket.Connected)
                {
                    socket.Close();
                    socket = null;
                    if (OnDisconnect != null)
                    {
                        OnDisconnect(this);
                    }
                }
            }
            catch { }
        }

        public void Send(PacketDataReq request)
        {
            byte[] data = createProtocolPacket(request);
            Send(data, 0, data.Length);
            data = null;
        }

        public void SendFile(byte[] readBytes, string fileName)
        {
            byte[] data = createFilePacket(readBytes, fileName);
            Send(data, 0, data.Length);
            data = null;
        }

        public void Send(byte[] data, int index, int length)
        {
            if (!Connected) return;

            socket.BeginSend(data, index, length, SocketFlags.None, sendCallBack, null);
        }

        public void sendCallBack(IAsyncResult ar)
        {
            try
            {
                int sent = socket.EndSend(ar);
                if (OnSend != null)
                {
                    OnSend(this, sent);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("SEND ERROR\n{0}", ex.Message));
            }
        }

        public byte[] createProtocolPacket(PacketDataReq request)
        {
            string msg = JsonConvert.SerializeObject(request);
            byte[] contentsBuffer = System.Text.Encoding.UTF8.GetBytes(msg);

            Header header = new Header();
            header.conetentsSize = convertEndian(contentsBuffer.Length);
            header.msgType = (int)MsgType.String;
            //LogHelper.log("TcpParser createProtocolPacket", Convert.ToString(contentsBuffer.Length));

            byte[] headerBuffer = structureToByte(header);
            byte[] packet = new byte[contentsBuffer.Length + HEADER_SIZE];
            headerBuffer.CopyTo(packet, 0);
            contentsBuffer.CopyTo(packet, HEADER_SIZE);

            return packet;
        }

        public byte[] createFilePacket(byte[] binaryFile, string fileName)
        {
            Header header = new Header();
            header.conetentsSize = convertEndian(binaryFile.Length);
            header.msgType = (int)MsgType.File;

            header.fileNameSize = System.Text.Encoding.Default.GetByteCount(fileName);
            header.fileName = fileName;

            byte[] headerBuffer = structureToByte(header);
            byte[] packet = new byte[binaryFile.Length + HEADER_SIZE];

            Array.Copy(headerBuffer, 0, packet, 0, HEADER_SIZE);
            Array.Copy(binaryFile, 0, packet, HEADER_SIZE, binaryFile.Length);
            
            return packet;
        }

        int totalBytes = 0;
        int readBytes = 0;

        public void StartRecv(Socket socket)
        {
            byte[] headerBuffer = new byte[HEADER_SIZE];
            totalBytes = headerBuffer.Length;
            readBytes = 0;

            StateObject state = new StateObject();
            state.workSocket = socket;
            state.buffer = headerBuffer;

            try
            {
                socket.BeginReceive(headerBuffer, 0, HEADER_SIZE, 0, new AsyncCallback(RecvHeaderCallback), state);
            }
            catch (SocketException se)
            {
                this.Disconnect();

                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionRefused:
                    case SocketError.ConnectionReset:
                        if (OnDisconnectByServer != null)
                        {
                            OnDisconnectByServer(this);
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Error[StartRecv] : {0}", ex.Message));
            }
        }

        private void RecvHeaderCallback(IAsyncResult ar)
        {
            if (RecvCompleted(ar, RecvHeaderCallback))
            {
                RecvContents(ar);
            }
        }

        private bool RecvCompleted(IAsyncResult ar, AsyncCallback cb)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;

                int bytesRead = state.workSocket.EndReceive(ar);
                if (bytesRead <= 0)
                {
                    //SHUTDOWN
                    LogHelper.log("TcpParser RecvCompleted", "Recv EOF");
                    return false;
                }

                readBytes += bytesRead;
                if (readBytes < totalBytes)
                {
                    LogHelper.log("TcpParser RecvCompleted", String.Format("readBytes < totalBytes - totalBytes: {0}, readBytes: {1}, bytesRead: {2}", totalBytes, readBytes, bytesRead));
                    state.workSocket.BeginReceive(state.buffer, readBytes, totalBytes - readBytes, 0, cb, state);
                }
                else
                {
                    LogHelper.log("TcpParser RecvCompleted success", String.Format("totalBytes: {0}, readBytes: {1}, bytesRead: {2}", totalBytes, readBytes, bytesRead));
                    return true;
                }
            }
            catch (SocketException se)
            {
                this.Disconnect();

                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionRefused:
                    case SocketError.ConnectionReset:
                        if (OnDisconnectByServer != null)
                        {
                            OnDisconnectByServer(this);
                            return false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Error[RecvCompleted] : {0}", ex.Message));
            }

            return false;
        }

        private void RecvContents(IAsyncResult result)
        {
            StateObject state = (StateObject)result.AsyncState;
            byte[] headerBuffer = state.buffer;

            Header header = new Header();
            header.conetentsSize = convertEndian(BitConverter.ToInt32(headerBuffer, 0));
            header.msgType = BitConverter.ToInt32(headerBuffer, 4);

            if (header.conetentsSize < 1)
            {
                LogHelper.log("TcpParser RecvContents", "invalid contentSize: " + header.conetentsSize);
                StartRecv(state.workSocket);
                return;
            }

            state.msgType = header.msgType;
            byte[] contentsBuffer = new byte[header.conetentsSize];
            state.buffer = contentsBuffer;
            totalBytes = contentsBuffer.Length;
            readBytes = 0;

            try
            {
                state.workSocket.BeginReceive(state.buffer, 0, totalBytes, 0, RecvContentsCallback, state);
            }
            catch (SocketException se)
            {
                this.Disconnect();

                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionRefused:
                    case SocketError.ConnectionReset:
                        if (OnDisconnectByServer != null)
                        {
                            OnDisconnectByServer(this);
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Error[RecvContents] : {0}", ex.Message));
            }
        }

        private void RecvContentsCallback(IAsyncResult result)
        {
            if (RecvCompleted(result, RecvContentsCallback))
            {
                StateObject state = (StateObject)result.AsyncState;

                //System.IO.MemoryStream stream = new System.IO.MemoryStream(state.buffer);
                StartRecv(state.workSocket);

                OnReceive(this, state.msgType, state.buffer);
            }
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
    }
}
