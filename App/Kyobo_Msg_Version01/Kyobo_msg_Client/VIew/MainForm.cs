using DSDO.COMMON.LIBRARY.Network;
using DSDO.COMMON.UTIL.Network;
using KSM.DAO.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Timers;
using System.Windows.Forms;
/*
 * DATE : 2020.08.10 
 * FORM_TITLE : 클라이언트 메인창
 * AUTHOR : 교보정보통신 김현수
 * DESC : 클라이언트에서 사용할 모든 처리를 구현한 FORM
 */
namespace Kyobo_Msg_Client
{
    public delegate void DataPushEventHandler(string value, string roomNo, String sendUserId);
    public delegate void DataGetEventHandlder(string value, string roomNo, String sednUserId);

    public delegate void PhotoChangeEventHandler(string value, string roomNo, String sendUserId);

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class MainForm : Form
    {
        //채팅창과의 연결이벤트
        public DataPushEventHandler DataSendEvent;
        public PhotoChangeEventHandler PhotoChangeEvent;

        //전역변수
        //Client client;
        private List<String> presentSessionList;
        public string data;        
        private string serverIP;
        private string serverPort;
        public String _userId;
        public String _userpass;
        public String _userName;

        //DAO연결
        _Sel _sel = new _Sel();
        _Ins _ins = new _Ins();
                
        private static System.Timers.Timer timerConnectServer; //Timer 개체 생성
        CommonUtil _cu = new CommonUtil();
        public MainForm(String[] param)
        {
            InitializeComponent();
            
            //서버와의 연동처리
            ConnectServer();
            
            //WEB에서 FORM메소드를 호출할 수 있도록 처리
            webBrowser1.ObjectForScripting = this;

            cbx01.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx01.SelectedIndex = 0;

            //로그인 정보를 저장
            serverIP = param[9];
            serverPort = param[10];
            _userId = param[0];
            _userpass = param[11];
            _userName = param[2];

            lab_name.Text = _userName + "님 반갑습니다.";
            lab_loginID.Text = _userId;

            //로그인 사용자 사진처리
            SetPhoto(_userId);

            //공지사항 처리
            ShowBoard(true);
           
            //신규 메세지 확인
            getReadMessage();

        }

        //로그인 memberid, 선택된 유저, 처리구분 kb
        public void RunApp(String userId, String selectUserList, String kb)
        {
            Hashtable _param = new Hashtable();
            UserDetailInfo udll = new UserDetailInfo();

            udll = MainProg.getUserInfoByKey("MEMBERID", userId.Substring(1));

            //메세지보내기
            if (kb.Equals("sendMessage"))
            {
                //MessageFormBox mfb = new MessageFormBox(userId, selectUserList, client, null);
                MessageFormBox mfb = new MessageFormBox(userId, selectUserList, null);
                mfb.Show();
            }
            //유저정보보기
            else if (kb.Equals("viewUserInfo"))
            {
                //선택한 유저 정보 가져오기
                UserInfo uif = new UserInfo(udll);
                uif.Show();
            }
            //채팅하기
            else if (kb.Equals("sendChat"))
            {

                List<String> _ru = new List<string>();

                int _personCnt = 0;

                if (selectUserList != null && selectUserList.Length > 0)
                {
                    String[] rcvUsrId = selectUserList.Split('^');

                    //체크박스로 선택한 사람들
                    foreach (String item in rcvUsrId)
                    {
                        //체크안되고 선택되어진 인원 중복 제외
                        if (!item.Equals(userId) && item.Length > 0)
                        {
                            String memberId = MainProg.getUserInfoByKey("MEMBERID", item.Substring(1)).MEMBERID;
                            if (presentSessionList.Contains(memberId))
                            {
                                ++_personCnt;
                                //유저실제아이디로 치환(SESSION_KEY값)
                                _ru.Add(memberId);
                            }
                        }
                    }
                    //채팅 선택되어진 사람(체크박스없이)
                    if (presentSessionList.Contains(udll.MEMBERID))
                    {
                        ++_personCnt;
                        //유저실제아이디로 치환(SESSION_KEY값)
                        _ru.Add(udll.MEMBERID);
                    }
                }
                else
                {
                    if (presentSessionList.Contains(udll.MEMBERID))
                    {
                        ++_personCnt;
                        //유저실제아이디로 치환(SESSION_KEY값)
                        _ru.Add(udll.MEMBERID);
                    }
                }

                //로그인된사람이 하나도 없으면 채팅불가
                if (_personCnt < 1)
                {
                    MessageBox.Show("로그인 된 참여자가 존재하지 않습니다.");
                    return;
                }                
               
                //채팅창 만든사람
                _ru.Add(_userId);

                //채팅창을 오픈
                ChatForm cf = new ChatForm(true, null, _userId, _ru);

                //채팅창에 받은 메세지를 넘겨줄 수 있도록 처리(MAINFORM => CHATFORM)
                this.DataSendEvent += new DataPushEventHandler(cf.SetActiveValue);
                cf.DataSendEvent += new DataGetEventHandlder(this.DataAction);

                cf.Show();
            }
        }

        //공지사항
        //처음 로그인 시에만 않읽은 공지사항이 있으면 팝업처리한다.
        public void ShowBoard(Boolean initFg)
        {   
            lv_notice.Items.Clear();

            lv_notice.View = View.Details;

            lv_notice.GridLines = true;
            lv_notice.FullRowSelect = true;

            Hashtable _param = new Hashtable();
            _param.Add("MESSAGE_KB","0");
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderList");

            if(_list.Count > 0)
            {
                foreach (Hashtable _item in _list)
                {
                    String[] _boardItem = {
                                            _cu.rtnHtS(_item["C_INDEX"]),
                                            _cu.rtnHtS(_item["TITLE"]),
                                            _cu.rtnHtS(_item["NAME"]),
                                            _cu.ToYYYYMMDD(_cu.rtnHtS(_item["REGI_DT"]), true),
                                            _cu.rtnHtS(_item["REGI_ID"])
                                          };
                    ListViewItem _itemList = new ListViewItem(_boardItem);

                    lv_notice.Items.Add(_itemList);
                }
            }

            //새로운 공지사항을 팝업으로 띄운다.
            if (initFg)
            {
                IList<Hashtable> _newnoticeList = _list.Where(w => !_cu.rtnHtS(w["ID_READER"]).Split(',').Contains(MainProg.GetUserId())).ToList<Hashtable>();

                foreach (Hashtable _item in _newnoticeList)
                {
                    NoticeView nv = new NoticeView(_cu.rtnHtS(_item["C_INDEX"]));
                    nv.Show();
                }
            }
        }

        //공지사항 오른쪽 버튼 클릭시
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
       
        //다른 시스템 연결 버튼 클릭시 (POST 방식)
        private void btn_Click(object sender, EventArgs e)
        { 
            String url = String.Empty;
            string str_sendvalue = "MGS_USERID=" + _userId + "&MGS_PASS=" + _userpass;

            string s = (sender as Button).Name;

            switch (s)
            {
                case "btn_webHard":
                    url = @"http://Webfile.korcham.net/ssopage_post.php";
                    str_sendvalue = "id=" + _userId + "&pass=" + _userpass;
                    break;
                case "btn_hompage":
                    url = "http://license.korcham.net";
                    break;
                case "btn_mgs":
                    url = @"https://mgsssl.korcham.net";
                    str_sendvalue = "MGS_USERID=" + _userId + "&MGS_PASS=" + _userpass;
                    break;
                case "btn_role":
                    url = "http://license.korcham.net/kor/privacy/roleInfo.jsp";
                    break;
                case "btn_schedule":
                    url = "http://license.korcham.net/ct/examSchedule.do";
                    break;
                default:
                    break;
            }
            SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorer();

            object vFlags = null;
            object vTargetFrame = null;
            object vUrl = url;
            object vHeaders = "Content-Type: application/x-www-form-urlencoded" + Convert.ToChar(10) + Convert.ToChar(13);
            var cPostData = str_sendvalue;
            object vPost = ASCIIEncoding.ASCII.GetBytes(cPostData);

            ie.Visible = true;
            ie.Navigate2(ref vUrl, ref vFlags, ref vTargetFrame, ref vPost, ref vHeaders);
        }

        //메인 창 닫혀질 경우
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            KICO메신저.Visible = false;
            KICO메신저.Dispose();
            //this.Dispose();
            System.Windows.Forms.Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            KICO메신저.Visible = false;
        }

        //트레이로 이동
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                KICO메신저.Visible = true;
            }
        }

        // 채팅서버에 연결처리
        private void ConnectServer()
        {
            //Session 만들기
            MainProg.client = new Client();
            //client.OnDisconnect += new Client.OnDisconnectEventHandler(OnDisConnect);
            //client.OnDisconnectByServer += new Client.OnDisconnectByServerEventHandler(OnDisConnectByServer);
            //연결시 이벤트
            MainProg.client.OnConnect += new Client.OnConnectEventHandler(OnConnect);
            //메세지 보내기
            MainProg.client.OnSend += new Client.OnSendEventHandler(OnSend);
            //메세지 받기
            MainProg.client.OnReceive += new Client.OnReceiveEventHandler(OnReceive);

            //서버와의 연결상태 체크
            timerConnectServer = new System.Timers.Timer(1000);
            timerConnectServer.Elapsed += new ElapsedEventHandler(ConnectServer);
            timerConnectServer.Start();
        }

        //void OnDisConnect(Client sender)
        //{
        //    timerConnectServer.Stop();
        //}
        //void OnDisConnectByServer(Client sender)
        //{
        
        //}

        //접속시 처리 이벤트
        void OnConnect(DSDO.COMMON.LIBRARY.Network.Client sender, bool connected)
        {
            PacketData_ConnectReq req = new PacketData_ConnectReq();
            req.userId = _userId;
            MainProg.client.Send(req);
        }

        //데이터 전송시 발생 이벤트 (후)
        void OnSend(Client sender, int sent)
        {
            Invoke((MethodInvoker)delegate
            {
                //toolStripStatusLabel2.Text = string.Format("Data Sent:{0}", sent);
            });
        }

        //데이터 받았을 때 발생하는 이벤트
        void OnReceive(Client sender, int msgType, byte[] buff)
        {
            string s = System.Text.Encoding.UTF8.GetString(buff);
            JObject jObject = JObject.Parse(s);
            int packetID = (int)jObject["PacketID"];

            PacketResponse response = new PacketResponse((PacketID)packetID, s);

            if (packetID == (int)PacketID.PacketID_Connect_Req)
            {
                PacketData_ConnectReq req = response.Parsing<PacketData_ConnectReq>();
                String _connetedUsers = String.Empty;
                Invoke((MethodInvoker)delegate
                {
                    //로그인한 사람 저장
                    presentSessionList = req.presentSessionList;
                });     
                foreach(String users in presentSessionList)
                {
                    _connetedUsers += "F" + users + "^";
                }

                //로그인하면 화면 처리(로그인한사람)
                this.Invoke(new Action(() => { webBrowser1.Document.InvokeScript("onLineMark", new object[] { _connetedUsers }); }));
            }
            else if(packetID == (int)PacketID.PacketID_DisConnect_Req)
            {
                PacketData_DisConnectReq req = response.Parsing<PacketData_DisConnectReq>();
                String _connetedUsers = String.Empty;
               
                _connetedUsers += "F" + req.userId + "^";

                //로그아웃이나 종료하면 화면 처리(로그인한사람)
                this.Invoke(new Action(() => { webBrowser1.Document.InvokeScript("offLineMark", new object[] { _connetedUsers }); }));
            }
            //공지사항 등록 및 수정시
            else if (packetID == (int)PacketID.PacketID_Send_Notice)
            {
                PacketData_SendNotice req = response.Parsing<PacketData_SendNotice>();
                Invoke((MethodInvoker)delegate
                {
                    ShowBoard(false);
                    NoticeView nv = new NoticeView(req.boardNo.ToString());
                    nv.Show();
                });
            }
            //메세지 받기
            else if (packetID == (int)PacketID.PacketID_Send_Msg)
            {
                PacketData_SendMessage req = response.Parsing<PacketData_SendMessage>();
                Invoke((MethodInvoker)delegate
                {
                    MessageView mv = new MessageView(req.boardNo.ToString());
                    mv.Show();
                });
            }
            //채팅방 만들기
            else if(packetID == (int)PacketID.PacketID_Make_Room)
            {
                PacketData_MakeRoom req = response.Parsing<PacketData_MakeRoom>();
                Invoke((MethodInvoker)delegate
                {
                    if (!_userId.Equals(req.makeUserId))
                    {
                        ChatForm cf = new ChatForm(false, req, _userId, null);

                        this.DataSendEvent += new DataPushEventHandler(cf.SetActiveValue);
                        cf.DataSendEvent += new DataGetEventHandlder(this.DataAction);

                        cf.Show();
                    }
                });
            }
            //방없애기
            else if (packetID == (int)PacketID.PacketID_Close_Room)
            {
                PacketData_CloseRoom req = response.Parsing<PacketData_CloseRoom>();
                Invoke((MethodInvoker)delegate
                {
                    DataSendEvent("close_theRoom", req.roomNo, req.userId);
                });
            }
            //방채팅
            else if (packetID == (int)PacketID.PacketID_Room_Msg)
            {
                PacketData_SendRoomMessage req = response.Parsing<PacketData_SendRoomMessage>();
                Invoke((MethodInvoker)delegate
                {
                    rb2.Text += req.message;
                    DataSendEvent(req.message, req.roomNo, req.sendUserId);
                });
            }
            //조직도 Reload
            else if (packetID == (int)PacketID.PacketID_ReloadGroup)
            {
                //화면 리로드 처리
                webBrowser1.Refresh();
            }
            //else
            //{
            //    RecvConnectAck(sender, response);
            //}
        }

        //void RecvConnectAck(Client sender, PacketResponse response)
        //{
        //    PacketData_ConnectAck ack = response.Parsing<PacketData_ConnectAck>();
        //    if (ack.result != PacketDataBase.Success)
        //    {
        //        string errMsg = PacketDataBase.ErrMsg_Unknown;

        //        if (ack.result == PacketDataBase.Err_WrongSeatNo)
        //            errMsg = PacketDataBase.ErrMsg_WrongSeatNo;
        //        else if (ack.result == PacketDataBase.Err_ExistSeatNo)
        //            errMsg = PacketDataBase.ErrMsg_ExistSeatNo;

        //        MessageBox.Show(errMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //}

        //서버연결 이벤트
        void ConnectServer(object sender, ElapsedEventArgs e)
        {
            if (timerConnectServer.Interval != 5000)
            {
                timerConnectServer.Interval = 5000; //5초 
            }            
            try
            {
                if (!MainProg.client.Connected)
                {
                    ClientConfig cConf = MainProg.CConf;
                    MainProg.client.Connect(serverIP, Int32.Parse(serverPort));
                }
                else
                {
                    PacketData_Ping5Sec req = new PacketData_Ping5Sec();
                    req.nowTime = System.DateTime.Now.Second;
                    MainProg.client.Send(req);
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }
            catch (Exception ex)
            {  
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }

            System.Windows.Forms.Application.DoEvents();

        }

        //체팅화면에 데이터 전송
        private void DataAction(string messaage, string roomNo, string sendUserId)
        {   
            data = messaage;
        }

        //사진가져오기
        public void SetPhoto(String userId)
        {
            string _photoPath = MainProg.CConf.FtpPath+"MPhoto/";
            string _fileName = userId + ".jpg";

            byte[] _imgByte = _cu.GetImgByte(_photoPath, MainProg.CConf.FtpUser, MainProg.CConf.FtpPass, _fileName);
            profileBox.Image = _cu.ByteToImage(_imgByte);
            profileBox.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        //공지사항 추가 버튼 클릭시
        private void noticeAdd_Click(object sender, EventArgs e)
        {
            NoticeWrite ni = new NoticeWrite(null);
            ni.DataPushEvent_Sub += new NoticeWrite.Sub_DataPushEventHandler(ShowBoard);
            ni.Show();
        }

        //공지사항 보기이벤트
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String noticeNum = string.Empty;
            if (lv_notice.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = lv_notice.SelectedItems;

                foreach(ListViewItem item in items)
                {
                    noticeNum = item.SubItems[0].Text;
                }
            }

            NoticeView nv = new NoticeView(noticeNum);           
            nv.Show();
        }

        //공지사항 삭제 이벤트
        private void noticeDel_Click(object sender, EventArgs e)
        {
            String noticeNum = string.Empty;
            if (lv_notice.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = lv_notice.SelectedItems;

                foreach (ListViewItem item in items)
                {
                    noticeNum = item.SubItems[0].Text;

                    if(!item.SubItems[4].Text.Equals(MainProg.GetUserId())) {
                        MessageBox.Show("작성자만 삭제할 수 있습니다.");
                        return;
                    }
                }

                if (MessageBox.Show("선택하신 공지를 삭제하시겠습니까?", "YesOrNo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("C_INDEX", noticeNum);
                    _ins.DeleteHashData(_param, "delBoard");

                    MessageBox.Show("삭제되었습니다.");
                    ShowBoard(false);
                }
            }
        }

        //공지사항 수정 이벤트
        private void noticeModify_Click(object sender, EventArgs e)
        {
            String noticeNum = string.Empty;
            if (lv_notice.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = lv_notice.SelectedItems;

                foreach (ListViewItem item in items)
                {
                    noticeNum = item.SubItems[0].Text;

                    if (!item.SubItems[4].Text.Equals(MainProg.GetUserId()))
                    {
                        MessageBox.Show("작성자만 수정할 수 있습니다.");
                        return;
                    }
                }

                NoticeWrite ni = new NoticeWrite(noticeNum);
                ni.DataPushEvent_Sub += new NoticeWrite.Sub_DataPushEventHandler(ShowBoard);
                ni.Show();
            }
        }

        //메세지 관리 폼 열기
        private void btn_message_mgs_Click(object sender, EventArgs e)
        {
            MessageMng mn = new MessageMng();
            mn.ShowDialog();
        }

        //로그인 시 읽지 않은 메세지 표시
        private void getReadMessage()
        {
            Hashtable _param = new Hashtable();
            _param.Add("REGI_ID", MainProg.GetUserId());
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderDetail");
            
            foreach(Hashtable _item in _list)
            {
                MessageView mv = new MessageView(_cu.rtnHtS(_item["C_INDEX"]));
                mv.Show();
            }
        }

        private void btn_UserInfo_Click(object sender, EventArgs e)
        {
            UserDetailInfo udll = new UserDetailInfo();
            //선택한 유저 정보 가져오기
            udll = MainProg.getUserInfoByKey("MEMBERID", MainProg.GetUserId());
            UserInfo uif = new UserInfo(udll);
            uif.Owner = this;
            uif.Show();
        }
        //공지사항 검색
        private void btn_search_Click(object sender, EventArgs e)
        {
            lv_notice.Items.Clear();
            lv_notice.View = View.Details;

            lv_notice.GridLines = true;
            lv_notice.FullRowSelect = true;

            Hashtable _param = new Hashtable();
            _param.Add("MESSAGE_KB", "0");
            _param.Add("SEARCHKB", cbx01.SelectedIndex);
            _param.Add("SEARCHTXT", txt_notice_search.Text.Trim());

            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderList");

            if (_list.Count > 0)
            {
                foreach (Hashtable _item in _list)
                {
                    String[] _boardItem = {
                                            _cu.rtnHtS(_item["C_INDEX"]),
                                            _cu.rtnHtS(_item["TITLE"]),
                                            _cu.rtnHtS(_item["NAME"]),
                                            _cu.ToYYYYMMDD(_cu.rtnHtS(_item["REGI_DT"]), true),
                                            _cu.rtnHtS(_item["REGI_ID"])
                                          };
                    ListViewItem _itemList = new ListViewItem(_boardItem);

                    lv_notice.Items.Add(_itemList);
                }
            }
        }

        private void mouse_hover(object sender, EventArgs e)
        {
            String url = String.Empty;
            string s = (sender as Button).Name;

            switch (s)
            {
                case "btn_webHard":
                    this.toolTip1.ToolTipTitle = "상의웹하드";
                    this.toolTip1.IsBalloon = true;
                    break;
                case "btn_hompage":
                    this.toolTip1.ToolTipTitle = "접수홈페이지";
                    this.toolTip1.IsBalloon = true;
                    break;
                case "btn_mgs":
                    this.toolTip1.ToolTipTitle = "자격관리시스템";
                    this.toolTip1.IsBalloon = true;
                    break;
                case "btn_role":
                    this.toolTip1.ToolTipTitle = "담당업무 및 담당자";
                    this.toolTip1.IsBalloon = true;
                    break;
                case "btn_schedule":
                    this.toolTip1.ToolTipTitle = "시험일정";
                    this.toolTip1.IsBalloon = true;
                    break;
                default:
                    break;
            }
        }

        //공지사항 그리드 제목틀
        private void lv_notice_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Pink, e.Bounds);
            e.DrawText();
        }
    }
}
