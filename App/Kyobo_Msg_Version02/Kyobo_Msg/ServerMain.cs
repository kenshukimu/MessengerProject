using System;
using DSDO.COMMON.UTIL.Network;
using DSDO.COMMON.LIBRARY.Network;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Timers;
using System.Collections;
using KSM.DAO.Models;

/*
* DATE : 2020.08.10 
* FORM_TITLE : 서버 기본 폼
* AUTHOR : 교보정보통신 김현수
* DESC : 서버 시작 및 기타 처리 폼
*/
namespace Kyobo_Msg_Server
{
    public partial class ServerMain : Form
    {
        Listener listener;
        static List<Session> sessionList;

        static List<Room> roomList;
        private static System.Timers.Timer timerConnectServer; //Timer 개체 생성

        DbRelation _db = new DbRelation();

        public ServerMain()
        {
            InitializeComponent();
            lv_roomList.View = View.Details;

            FormClosing += new FormClosingEventHandler(FrmMainControl_FormClosing);

            sessionList = new List<Session>();
            roomList = new List<Room>();

            string localIP = "Not available, please check your network seetings!";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            lab_ip.Text = localIP;
            lab_port.Text = ConfigurationManager.AppSettings["ServerPORT"];

            IList<Hashtable> _getMethod = new _Sel().GetListHashData(null, "getRunMethod");

            if (_getMethod == null) MessageBox.Show("연결안됨");

        }
        #region 이벤트
        void FrmMainControl_FormClosing(object sender, EventArgs e)
        {
            StopListen();
            Application.Exit();
        }
        #endregion

        #region 메소드
        //서버 리스너 정지
        void StopListen()
        {
            foreach (Session session in sessionList)
            {
                if (session != null)
                {
                    session.Close();
                }
            }
            sessionList.Clear();

            if (listener != null && listener.Running)
            {
                listener.Stop();
                listener = null;
                timerConnectServer.Dispose();
            }

            Invoke((MethodInvoker)delegate
            {
                tssl.Text = "Server Stop Listen!!";
                lvMainControl.Items.Clear();
                lv_roomList.Items.Clear();
                recvBox.Text = "";
                serverStartTime1.Text = "";
                serverStartTime2.Text = "";
                lab_presentCnt.Text = "0명";
                btn_ServerStart.BackgroundImage = Kyobo_Msg.Properties.Resources.start_server;
            });
        }

        //서버 리스너 시작
        void StartListen()
        {
            StopListen();

            int port = int.Parse(ConfigurationManager.AppSettings["ServerPORT"]);   // App.config 13000
          
            listener = new Listener();
            listener.Accepted += new Listener.SocketAcceptedHandler(SocketAccepted);
            listener.Start(port);

            Invoke((MethodInvoker)delegate
            {
                tssl.Text = "Server Start Listen!!";

                btn_ServerStart.BackgroundImage = Kyobo_Msg.Properties.Resources.stop_server;
                serverStartTime1.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
                serverStartTime2.Text = DateTime.Now.ToString("HH시 mm분 ss초 실행");
                lab_presentCnt.Text = "0명";
            });

            timerConnectServer = new System.Timers.Timer(100000);
            timerConnectServer.Elapsed += new ElapsedEventHandler(getRunMethod);
            timerConnectServer.Start();
        }

        void SocketAccepted(Socket e)
        {
            Thread sessionWorker = new Thread(new ParameterizedThreadStart(SocketWorkThread));
            sessionWorker.IsBackground = true;
            sessionWorker.Start(e);
        }

        void SocketWorkThread(Object sender)
        {
            Session session = new Session((Socket)sender);
            sessionList.Add(session);

            session.Disconnected += new Session.DisconnectedEventHandler(SessionDisConnected);
            session.DataReceived += new Session.DataReceivedEventHandler(SessionRecvData);
            session.ReceiveAsync();
        }

        //클라이언트 연결끊기 처리
        void SessionDisConnected(Session sender)
        {
            SetStatusMsg(sender.userId, "Disconnect", "PC 연결 끊어짐", false);

            sessionList.Remove(sender);
            //로그아웃 처리
            PacketData_DisConnectReq req = new PacketData_DisConnectReq();
            //로그인 한 사람이 있으면 각 클라이언트 리스트를 전달한다.
            //현재로그인 인원
            req.userId = sender.userId;
            //로그인인원에게 리스트 갱신처리
            foreach (Session session in sessionList)
            {
                session.Send(req);
            }
            sender.Close();
        }

        //클라이언트 연결확인
        void RecvConnectReq(Session sender, PacketResponse response)
        {
            PacketData_ConnectReq req = response.Parsing<PacketData_ConnectReq>();

            PacketData_ConnectAck ack = new PacketData_ConnectAck();
            sender.SetSState(SState.State_Connect);

            String userId = req.userId;
            sender.userId = userId;

            SetStatusMsg(userId, "PC 연결 성공", sender.GetIpAddress(), true);
        }

        //클라이언트에서 넘어오는 데이터 처리
        void SessionRecvData(Session sender, RecvBuff e)
        {
            if (e.msgType == (int)MsgType.String)
            {
                string s = System.Text.Encoding.UTF8.GetString(e.memoryStream.ToArray());
                JObject jObject = JObject.Parse(s);
                Int32 packetID = (Int32)jObject["PacketID"];

                PacketResponse response = new PacketResponse((PacketID)packetID, s);

                if (packetID == (int)PacketID.PacketID_Connect_Req)
                {
                    PacketData_ConnectReq req = response.Parsing<PacketData_ConnectReq>();
                    RecvConnectReq(sender, response);                    

                    //로그인 한 사람이 있으면 각 클라이언트 리스트를 전달한다.
                    List<String> presentSessionList = new List<string>();
                    //현재로그인 인원
                    foreach (Session session in sessionList)
                    {
                        presentSessionList.Add(session.userId);
                    }

                    req.presentSessionList = presentSessionList;
                    //유저정보를 취득하여 보낸다.
                    req.userList = _db.getUserLIst();
                    //로그인인원에게 리스트 갱신처리
                    foreach (Session session in sessionList)
                    {
                        // 로그인한사람의 리스트 처리
                        if(session.userId.Equals(sender.userId))
                        {
                            //공지사항 리스트를 가져간다.
                            req.noticeList = _db.getNoticeList();
                            //메세지 리스트를 가져간다.
                            req.messageList = _db.getUnReadBoarderDetail(sender.userId);                           
                        }
                        else
                        {
                            req.noticeList = null;
                            req.messageList = null;
                        }

                        session.Send(req);
                    }                    
                }
                else if (packetID == (int)PacketID.PacketID_Notice_Detail)
                {
                    PacketData_NoticeDetail req = response.Parsing<PacketData_NoticeDetail>();
                    
                    Invoke((MethodInvoker)delegate
                    {
                        req.noticeDetail = _db.getNoticeDetail(req.noticeNum, sender.userId);

                        Session session = GetSessionFromList(sender.userId);
                        if (session != null)
                        {
                            session.Send(req);
                        }
                    });
                }
                else if (packetID == (int)PacketID.PacketID_Send_Notice)
                {
                    PacketData_SendNotice req = response.Parsing<PacketData_SendNotice>();
                    
                    int boardNo = 0;
                    if(req.noticeProcess.Equals("U"))
                    {
                        _db.updateNoticeDetail(req.noticeDetail);
                        boardNo = req.noticeNum;
                        req.noticeProcess = "R";
                    }
                    else if (req.noticeProcess.Equals("D"))
                    {
                        _db.deleteNoticeDetail(req.noticeNum);
                        req.noticeProcess = "D";
                    }
                    else
                    {
                        req.noticeDetail.Add("ID_READER", sender.userId);
                        _db.insertNoticeDetail(req.noticeDetail);
                        req.noticeProcess = "R";
                    }

                    //req.boardNo = boardNo;
                    //처리 후 읽기 모드                    
                    req.noticeList = _db.getNoticeList();
                    //req.noticeDetail_Rev = _db.getNoticeDetail(boardNo.ToString(), sender.userId);

                    Invoke((MethodInvoker)delegate
                    {
                        //로그인되어 있는 사람 모드에게 오픈처리
                        foreach (Session session in sessionList)
                        {
                            session.Send(req);
                        }
                    });
                }
                else if (packetID == (int)PacketID.PacketID_Notice_Find)
                {
                    PacketData_NoticeFind req = response.Parsing<PacketData_NoticeFind>();
                    req.noticeList = _db.getNoticeListFind(req.criFind);

                    Invoke((MethodInvoker)delegate
                    {                        
                        Session session = GetSessionFromList(sender.userId);
                        if (session != null)
                        {
                            session.Send(req);
                        }
                    });
                }
                //메세지 관리함
                else if (packetID == (int)PacketID.PacketID_Message_Mng)
                {
                    PacketData_MessageMng req = response.Parsing<PacketData_MessageMng>();
                    Invoke((MethodInvoker)delegate
                    {
                        req.messageList = _db.getBoarderRcvList(req.criInfo);

                        Session session = GetSessionFromList(sender.userId);
                        if (session != null)
                        {
                            session.Send(req);
                        }
                    });
                }
                //메세지 상세보기
                else if (packetID == (int)PacketID.PacketID_Message_Detail)
                {
                    PacketData_MessageDetail req = response.Parsing<PacketData_MessageDetail>();
                    Invoke((MethodInvoker)delegate
                    {
                        req.messageDetail = _db.getMessageDetail(req.criInfo);
                        req.messageReaderDetail = _db.getMessageReaderDetail(req.criInfo);

                        Session session = GetSessionFromList(sender.userId);
                        if (session != null)
                        {
                            session.Send(req);
                        }
                    });
                }
                //메세지 검색하기
                else if (packetID == (int)PacketID.PacketID_Message_Search)
                {
                    PacketData_MessageSearch req = response.Parsing<PacketData_MessageSearch>();
                    Invoke((MethodInvoker)delegate
                    {
                        req.messageList = _db.getBoarderRcvList(req.criInfo);
                        Session session = GetSessionFromList(sender.userId);
                        if (session != null)
                        {
                            session.Send(req);
                        }
                    });
                }
                //메세지 회신
                else if (packetID == (int)PacketID.PacketID_Message_ReWrite)
                {
                    PacketData_MessageReWrite req = response.Parsing<PacketData_MessageReWrite>();
                    req.messageDetail = _db.getMessageDetail(req.criInfo);
                    Invoke((MethodInvoker)delegate
                    {
                        Session session = GetSessionFromList(sender.userId);
                        if (session != null)
                        {
                            session.Send(req);
                        }
                    });
                }
                //메세지 보내기
                else if (packetID == (int)PacketID.PacketID_Send_Msg)
                {
                    PacketData_SendMessage req = response.Parsing<PacketData_SendMessage>();

                    int boardNo = _db.insertMessageDetail(req.messageDetail, req.revUserId, sender.userId);
                    req.boardNo = boardNo;

                    Invoke((MethodInvoker)delegate
                    {
                        foreach(String rcvUser in req.revUserId)
                        {
                            Session session = GetSessionFromList(rcvUser);
                            if (session != null)
                            {
                                session.Send(req);
                            }
                        }
                    });
                }
                //메세지 읽기 처리
                else if (packetID == (int)PacketID.PacketID_Send_Msg_Read)
                {
                    PacketData_SendMessage_Read req = response.Parsing<PacketData_SendMessage_Read>();
                    _db.updateBoardDetail(req.criInfo);
                }

                else if (packetID == (int)PacketID.PacketID_Make_Room)
                {
                    PacketData_MakeRoom req = response.Parsing<PacketData_MakeRoom>();
                    Invoke((MethodInvoker)delegate
                    { 
                        lv_roomList.BeginUpdate();

                        ListViewItem lvi = new ListViewItem(req.roomNo);
                        Room _room = new Room();
                        lv_roomList.Items.Add(lvi);

                        List<Session> _roomSessionList = new List<Session>();

                        foreach (String rcvUser in req.revUserId)
                        {
                            Session session = GetSessionFromList(rcvUser);
                            if (session != null)
                            {
                                _roomSessionList.Add(session);
                                session.Send(req);
                            }
                        }
                        lv_roomList.EndUpdate();

                        //방의 소유주
                        _room.mainUserID = req.makeUserId;
                        _room.roomNo = req.roomNo;
                        _room.sessionList = _roomSessionList;
                        roomList.Add(_room);
                    });
                }
                else if (packetID == (int)PacketID.PacketID_Close_Room)
                {
                    PacketData_CloseRoom req = response.Parsing<PacketData_CloseRoom>();
                    Invoke((MethodInvoker)delegate
                    {
                        var toRemoveRoom = new List<String>();
                        foreach (Room _item in roomList)
                        {
                            if (_item.roomNo.Equals(req.roomNo))
                            {
                                int idx = 0;
                                var toRemoveSession = new List<String>();
                                while (idx < _item.sessionList.Count)
                                {
                                    Session session = _item.sessionList[idx];
                                    if (session.userId.Equals(req.userId))
                                    {
                                        toRemoveSession.Add(session.userId);
                                        session = null;
                                    }

                                    if (session != null)
                                    {
                                        session.Send(req);
                                    }
                                    ++idx;
                                }
                                _item.sessionList.RemoveAll(d=> toRemoveSession.Contains(d.userId));


                                if (_item.sessionList.Count < 1)
                                {  
                                    toRemoveRoom.Add(_item.roomNo);

                                    lv_roomList.BeginUpdate();

                                    for (int i = 0; i < lv_roomList.Items.Count; i++)
                                    {
                                        ListViewItem fItem = lv_roomList.Items[i];
                                        if (fItem.SubItems[0].Text.Equals(req.roomNo))
                                        {
                                            fItem.Remove();
                                            break;
                                        }
                                    }
                                    lv_roomList.EndUpdate();
                                }
                            }
                        }
                        roomList.RemoveAll(d => toRemoveRoom.Contains(d.roomNo));
                    });
                }
                else if (packetID == (int)PacketID.PacketID_Room_Msg)
                {
                    PacketData_SendRoomMessage req = response.Parsing<PacketData_SendRoomMessage>();
                    Invoke((MethodInvoker)delegate
                    {
                        recvBox.Text += req.message;

                        foreach (Room _item in roomList)
                        {
                            if (_item.roomNo.Equals(req.roomNo))
                            {
                                foreach (Session session in _item.sessionList)
                                {
                                    if (session != null)
                                    {
                                        session.Send(req);
                                    }
                                }
                            }
                        }
                    });
                }
            }
            else if (e.msgType == (int)MsgType.File)
            {                
            }
        }

        //접속되어 있는 사용자의 Session 정보 취득
        Session GetSessionFromList(String userId)
        {            
            foreach (Session session in sessionList)
            {
                if (session.userId == userId)
                    return session;
            }
            return null;
        }      

        //클라이언트에서 접속한 정보 화면에 표시
        void SetStatusMsg(string userId, string msg, String ip, Boolean recon)
        {
            Invoke((MethodInvoker)delegate
            {
                lvMainControl.BeginUpdate();

                for(int i=0; i< lvMainControl.Items.Count;i++)
                {
                    ListViewItem fItem = lvMainControl.Items[i];
                    if(fItem.SubItems[0].Text.Equals(userId)) {
                        fItem.Remove();
                        break;
                    }
                }

                if(recon) { 
                    ListViewItem lvi = new ListViewItem(userId);
                    lvi.SubItems.Add(msg);
                    lvi.SubItems.Add(ip);
                    lvi.SubItems.Add(DateTime.Now.ToString("yyyy년MM월dd일HH시mm분ss초"));

                    lvMainControl.Items.Add(lvi);
                }
               
                lvMainControl.EndUpdate();

                lab_presentCnt.Text = lvMainControl.Items.Count + "명";
            });
        }


        #endregion

        //화면 초기 로딩
        private void FrmMainControl_Load(object sender, EventArgs e)
        {
            lvMainControl.View = View.Details;
            lvMainControl.GridLines = true;
            lvMainControl.FullRowSelect = true;
        }

        //서버 시작 버튼 이벤트
        private void btn_ServerStart_Click(object sender, EventArgs e)
        {
            if (tssl.Text.Equals("Server Stop Listen!!"))
            {
                StartListen();
            }
            else
            {             
                StopListen();       
            }
        }

        void getRunMethod(object sender, ElapsedEventArgs e)
        {
            if (timerConnectServer.Interval != 300000)
            {
                timerConnectServer.Interval = 300000; //5분 
            }
            try
            {
                IList<Hashtable> _getMethod = new _Sel().GetListHashData(null, "getRunMethod");

                foreach (Hashtable _item in _getMethod)
                {
                    switch (_item["DATA"].ToString())
                    {
                        case "RELOAD_GROUPD":

                            PacketData_ReloadGroup req = new PacketData_ReloadGroup();
                            //로그인인원에게 리스트 갱신처리
                            foreach (Session session in sessionList)
                            {
                                session.Send(req);
                            }
                            break;
                        default:
                            break;
                    }
                    //한건만 실행처리
                    break;
                }
                new _Ins().DeleteHashData(null, "delRunMethod");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }

            System.Windows.Forms.Application.DoEvents();

        }
    }
}
