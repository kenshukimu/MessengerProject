using DSDO.COMMON.LIBRARY.Network;
using DSDO.COMMON.UTIL.Network;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * DATE : 2020.08.10 
 * FORM_TITLE : 채팅창
 * AUTHOR : 교보정보통신 김현수
 * DESC : 채팅을 할 수 있도록 구현한 FORM
 */
namespace Kyobo_Msg_Client
{
    public partial class ChatForm : Form
    {
        //부모창에서 전달받을 수 있도록 구현
        public DataGetEventHandlder DataSendEvent;       
        //채팅방 번호(개인 및 단체 채팅을 위한 방번호)
        protected String roomNo;

        public ChatForm()
        {
            InitializeComponent();
        }
        #region 채팅창 초기처리
        /*
         * DESC  : 채팅창 초기처리 
         * PARAM : [1] 자기SESSION 정보
         *         [2] 창이 열려있는지 여부
         *         [3] 방 개설 정보
         *         [4] 방 개설자 정보
         *         [5] 방 참여자 정보
         */
        public ChatForm(Boolean open,
                        PacketData_MakeRoom req,
                        String userId,
                        List<String> _ru)
        {
            InitializeComponent();

            //대화창에 참여하고 있는 명단 출력
            lv_chatPerson.View = View.Details;
            labUserId.Text = MainProg.getUserInfoByKey("MEMBERID", userId).MEMBERNAME;
            //방을 개설할 경우 처리
            if (open)
            {
                req = new PacketData_MakeRoom();
                Random r = new Random();
                req.roomNo = r.Next(0, 500).ToString();
                req.makeUserId = userId;
                roomNo = req.roomNo;
                req.revUserId = _ru;
                MainProg.client.Send(req);
            }
            else
            {
                //참여자 화면 처리
                roomNo = req.roomNo;
            }
            lv_chatPerson.BeginUpdate();

            String[] _boardItem = { MainProg.getUserInfoByKey("MEMBERID", req.makeUserId).MEMBERNAME, "[개설자]", req.makeUserId };
            ListViewItem _itemList = new ListViewItem(_boardItem);
            lv_chatPerson.Items.Add(_itemList);

            foreach (String rcvUser in req.revUserId)
            {
                if (rcvUser.Equals(req.makeUserId)) continue;

                _itemList = new ListViewItem(new String[] { MainProg.getUserInfoByKey("MEMBERID", rcvUser).MEMBERNAME, "[참여자]", rcvUser});
                lv_chatPerson.Items.Add(_itemList);
            }
            lv_chatPerson.EndUpdate();
        }
        #endregion
        #region 채팅 메세지 전송       
        private void btn_sendMessage_Click(object sender, EventArgs e)
        {
            PacketData_SendRoomMessage req = new PacketData_SendRoomMessage();
            req.sendUserId = labUserId.Text;
            req.message = sendTextBox.Text;
            req.roomNo = roomNo;
            MainProg.client.Send(req);

            sendTextBox.Text = "";
        }
        #endregion
        #region 다른 참여자가 전송한 데이터를 처리      
        public void SetActiveValue(string param, String _roomNo, String sendUserId)
        {
            if (_roomNo.Equals(roomNo)) {
                if (param.Equals("close_theRoom"))
                {
                    chatHisBox.Text += "[" + MainProg.getUserInfoByKey("MEMBERID", sendUserId).MEMBERNAME + "] 방에서 나갔습니다. ";
                    chatHisBox.Text += Environment.NewLine;
                    lv_chatPerson.Update();
                    for (int i = 0; i < lv_chatPerson.Items.Count; i++)
                    {
                        ListViewItem fItem = lv_chatPerson.Items[i];
                        if (fItem.SubItems[2].Text.Equals(sendUserId))
                        {
                            fItem.Remove();
                            break;
                        }
                    }
                    lv_chatPerson.EndUpdate();
                }
                else
                {
                    chatHisBox.Text += "[" + sendUserId + "] : ";
                    chatHisBox.Text += param;
                    chatHisBox.Text += Environment.NewLine;
                }
            }
        }
        #endregion
        #region 창을 닫을때 처리       
        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PacketData_CloseRoom req = new PacketData_CloseRoom();
            req.userId = MainProg.GetUserId();
            req.roomNo = roomNo;
            MainProg.client.Send(req);
        }
        #endregion
    }
}

