using System;
using System.Net;
using System.Net.Sockets;

/*
* DATE : 2020.08.10 
* FORM_TITLE : 접속 기본 클래스
* AUTHOR : 교보정보통신 김현수
* DESC : 서버와 클라이언트 연결처리
*/
namespace Kyobo_Msg_Server
{
    class Listener
    {
        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler Accepted;

        Socket listener;
        public int Port;

        public bool Running
        {
            get;
            private set;
        }

        public Listener() { Port = 0; }
        public void Start(int port)
        {
            if (Running)
                return;

            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            listener.Bind(new IPEndPoint(IPAddress.Any, port));
            listener.Listen(0);

            listener.BeginAccept(acceptedCallback, null);
            Running = true;
        }

        public void Stop()
        {
            if (!Running)
                return;

            listener.Close();
            Running = false;
        }

        void acceptedCallback(IAsyncResult ar)
        {
            try
            {
                Socket s = listener.EndAccept(ar);

                if (Accepted != null)
                {
                    Accepted(s);
                }
            }
            catch { }

            if(Running)
            {
                try
                {
                    listener.BeginAccept(acceptedCallback, null);
                }
                catch { }
            }
        }
    }
}
