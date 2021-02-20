using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Diagnostics;
using DSDO.COMMON.LIBRARY.Network;
//using KSM.DAO.Models;
using DSDO.COMMON.UTIL.Network;

namespace Kyobo_Msg_Client
{
   
    public partial class MessageMng : Form
    {
        //부모창에서 전달받을 수 있도록 구현
        public DataGetMessageEventHandlder DataSendMessageEvent;

        ClientConfig cf = MainProg.CConf;
        //_Sel _sel = new _Sel();
        CommonUtil _cu = new CommonUtil();
        String strSpeed = "";
        String strLeftTime = "";

        //_Sel _sel = new _Sel();

        public MessageMng(IList<Hashtable> _list)
        {
            InitializeComponent();

            cbx01.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx01.SelectedIndex = 0;

            //리스트뷰 초기화처리
            lvRcv.View = View.Details;
            lvRcv.Items.Clear();
            lvRcv.GridLines = true;
            lvRcv.FullRowSelect = true;

            lvSend.View = View.Details;
            lvSend.Items.Clear();
            lvSend.GridLines = true;
            lvSend.FullRowSelect = true;

            lvRcvResult.View = View.Details;
            lvRcvResult.Items.Clear();
            lvRcvResult.GridLines = true;
            lvRcvResult.FullRowSelect = true;
            lvRcvResult.Columns.Add("받은사람", 150, HorizontalAlignment.Left);
            lvRcvResult.Columns.Add("받은일자", 200, HorizontalAlignment.Left);

            lvFileList.View = View.Details;
            lvFileList.Items.Clear();
            lvFileList.GridLines = true;
            lvFileList.FullRowSelect = true;
            lvFileList.Columns.Add("파일명", 200, HorizontalAlignment.Left);
            lvFileList.Columns.Add("0", 0, HorizontalAlignment.Left);

            lvFileList2.View = View.Details;
            lvFileList2.Items.Clear();
            lvFileList2.GridLines = true;
            lvFileList2.FullRowSelect = true;
            lvFileList2.Columns.Add("파일명", 200, HorizontalAlignment.Left);
            lvFileList2.Columns.Add("0", 0, HorizontalAlignment.Left);

            lb_userNm.BackColor = System.Drawing.Color.White;
            panel4.Visible = false;

            showMessageLIst(_list);
        }

        private void showMessageLIst(IList<Hashtable> _list)
        {
            //Hashtable _param = new Hashtable();
            //_param.Add("MESSAGE_KB", "1");
            //_param.Add("REGI_ID", MainProg.GetUserId());
            //IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderRcvList");

            if (_list == null) return;

            IList<Hashtable> _slist = _list.Where(w => w["REGI_ID"].ToString().Equals(MainProg.GetUserId())).ToList<Hashtable>();
            IList<Hashtable> _rlist = _list.Where(w => !w["REGI_ID"].ToString().Equals(MainProg.GetUserId())).ToList<Hashtable>();

            //보낸 리스트
            if(_slist.Count > 0)
            {
                
                foreach (Hashtable _item in _slist)
                {
                    String _rcvUserNm = String.Empty;
                    String[] _usersNm = _cu.rtnHtS(_item["ID_READER"]).Split(',');

                    foreach(String _user in _usersNm)
                    {
                        if (_user.Equals("")) continue;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(MainProg.getUserInfoByKey("MEMBERID", _user).MEMBERNAME);
                        sb.Append(";");
                        _rcvUserNm += sb.ToString();
                    }

                    String[] _boardItem = {
                                            _cu.rtnHtS(_item["C_INDEX"]),
                                            _rcvUserNm,
                                            //_cu.ToYYYYMMDD(_cu.rtnHtS(_item["REGI_DT"]), true)
                                            _cu.GetDateTimeFormat(DateTime.Parse(_cu.rtnHtS(_item["REGI_DT"])), "yyyy-mm-dd hh:nn:ss"),
                                          };
                    ListViewItem _itemList = new ListViewItem(_boardItem);

                    lvSend.Items.Add(_itemList);
                }

                //if (lvSend.Items.Count > 0) lvSend.Items[0].Selected = true;
            }
            //받은 리스트
            if (_rlist.Count > 0)
            {
                foreach (Hashtable _item in _rlist)
                {
                    String _rcvUserNm = String.Empty;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(MainProg.getUserInfoByKey("MEMBERID", _cu.rtnHtS(_item["REGI_ID"])).MEMBERNAME);
                    sb.Append(";");
                    _rcvUserNm += sb.ToString();

                    String[] _boardItem = {
                                            _cu.rtnHtS(_item["C_INDEX"]),
                                            _rcvUserNm,
                                            //_cu.ToYYYYMMDD(_cu.rtnHtS(_item["REGI_DT"]), true)
                                             _cu.GetDateTimeFormat(DateTime.Parse(_cu.rtnHtS(_item["REGI_DT"])), "yyyy-mm-dd hh:nn:ss")
                                          };
                    ListViewItem _itemList = new ListViewItem(_boardItem);

                    lvRcv.Items.Add(_itemList);
                }

                if (lvRcv.Items.Count > 0) lvRcv.Items[0].Selected = true;
            }
        }

        //받은 메세지 선택 표시
        private void lvRcv_SelectedIndexChanged(object sender, EventArgs e)
        {
            String messageNum = string.Empty;
            lvRcvResult.Items.Clear();
            lvFileList.Items.Clear();
            lb_cindex.Text = String.Empty;

            if (lvRcv.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = lvRcv.SelectedItems;

                foreach (ListViewItem item in items)
                {
                    messageNum = item.SubItems[0].Text;
                }

                Hashtable _param = new Hashtable();
                _param.Add("C_INDEX", messageNum);
                _param.Add("MESSAGE_KB", "1");

                PacketData_MessageDetail req = new PacketData_MessageDetail();               
                req.criInfo = _param;
                MainProg.client.Send(req);
            }
        }

        //보낸 메세지 선택 표시
        private void lvSend_SelectedIndexChanged(object sender, EventArgs e)
        {
            String messageNum = string.Empty;
            lvFileList.Items.Clear();
            lvRcvResult.Items.Clear();
            lb_cindex.Text = String.Empty;

            if (lvSend.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = lvSend.SelectedItems;

                foreach (ListViewItem item in items)
                {
                    messageNum = item.SubItems[0].Text;
                }

                Hashtable _param = new Hashtable();
                _param.Add("C_INDEX", messageNum);
                _param.Add("MESSAGE_KB", "1");

                PacketData_MessageDetail req = new PacketData_MessageDetail();
                req.criInfo = _param;
                MainProg.client.Send(req);
            }
        }

        public void SetActiveValue(IList<Hashtable> _message, IList<Hashtable> _readers, String process)
        {        
            if(process !=null && process.Equals("1")) {

                IList<Hashtable> _slist = _message.Where(w => w["REGI_ID"].ToString().Equals(MainProg.GetUserId())).ToList<Hashtable>();
                IList<Hashtable> _rlist = _message.Where(w => !w["REGI_ID"].ToString().Equals(MainProg.GetUserId())).ToList<Hashtable>();

                lb_userNm.Text = "-";
                lb_date.Text = "-";
                messageBody.DocumentText = "";
                lvFileList.Items.Clear();

                if (msgList.SelectedTab == msgList.TabPages["tabPage1"])
                {//받은메세지

                    lvRcv.Items.Clear();
                    foreach (Hashtable _item in _rlist)
                    {

                        String _rcvUserNm = String.Empty;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(MainProg.getUserInfoByKey("MEMBERID", _cu.rtnHtS(_item["REGI_ID"])).MEMBERNAME);
                        sb.Append(";");
                        _rcvUserNm += sb.ToString();

                        String[] _boardItem = {
                                            _cu.rtnHtS(_item["C_INDEX"]),
                                            _rcvUserNm,
                                            //_cu.ToYYYYMMDD(_cu.rtnHtS(_item["REGI_DT"]), true)
                                             _cu.GetDateTimeFormat(DateTime.Parse(_cu.rtnHtS(_item["REGI_DT"])), "yyyy-mm-dd hh:nn:ss")
                                          };
                        ListViewItem _itemList = new ListViewItem(_boardItem);

                        lvRcv.Items.Add(_itemList);
                    }

                    if (lvRcv.Items.Count > 0) lvRcv.Items[0].Selected = true;
                }
                else
                {//보낸메세지
                    lvSend.Items.Clear();

                    foreach (Hashtable _item in _slist)
                    {
                        String _rcvUserNm = String.Empty;
                        String[] _usersNm = _cu.rtnHtS(_item["ID_READER"]).Split(',');

                        foreach (String _user in _usersNm)
                        {
                            if (_user.Equals("")) continue;
                            StringBuilder sb = new StringBuilder();
                            sb.Append(MainProg.getUserInfoByKey("MEMBERID", _user).MEMBERNAME);
                            sb.Append(";");
                            _rcvUserNm += sb.ToString();
                        }

                        String[] _boardItem = {
                                            _cu.rtnHtS(_item["C_INDEX"]),
                                            _rcvUserNm,
                                            //_cu.ToYYYYMMDD(_cu.rtnHtS(_item["REGI_DT"]), true)
                                            _cu.GetDateTimeFormat(DateTime.Parse(_cu.rtnHtS(_item["REGI_DT"])), "yyyy-mm-dd hh:nn:ss")
                                          };
                        ListViewItem _itemList = new ListViewItem(_boardItem);

                        lvSend.Items.Add(_itemList);
                    }
                    if (lvSend.Items.Count > 0) lvSend.Items[0].Selected = true;
                }
            }
            else
            {
                foreach (Hashtable _item in _readers)
                {
                    ListViewItem lvi = new ListViewItem(MainProg.getUserInfoByKey("MEMBERID", _cu.rtnHtS(_item["ID_READER"])).MEMBERNAME);
                    lvi.SubItems.Add(_cu.rtnHtS(_item["READ_TIME"]));
                    lvRcvResult.Items.Add(lvi);
                }

                foreach (Hashtable _item in _message)
                {
                    lb_cindex.Text = _cu.rtnHtS(_item["C_INDEX"]);

                    if (msgList.SelectedTab == msgList.TabPages["tabPage1"])
                    {
                        lb_userNm.Text = MainProg.getUserInfoByKey("MEMBERID", _cu.rtnHtS(_item["REGI_ID"])).MEMBERNAME;
                    }
                    else
                    {
                        lb_userNm.Text = MainProg.getUserInfoByKey("MEMBERID", _cu.rtnHtS(_item["ID_READER"])).MEMBERNAME;
                    }
                    //lb_date.Text = _cu.ToYYYYMMDD(_cu.rtnHtS(_item["REGI_DT"]), true);

                    lb_date.Text = _cu.GetDateTimeFormat(DateTime.Parse(_cu.rtnHtS(_item["REGI_DT"])), "yyyy-mm-dd hh:nn:ss");
                    messageBody.DocumentText = _cu.rtnHtS(_item["CONTEXT"]);

                    if (!_cu.rtnHtS(_item["FILE_"]).Equals(""))
                    {
                        ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_item["FILE_ORIGIN"]));
                        lvi.SubItems.Add(_cu.rtnHtS(_item["FILE_"]));
                        lvFileList.Items.Add(lvi);
                    }

                    if (!_cu.rtnHtS(_item["FILE1_"]).Equals(""))
                    {
                        ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_item["FILE1_ORIGIN"]));
                        lvi.SubItems.Add(_cu.rtnHtS(_item["FILE1_"]));
                        lvFileList.Items.Add(lvi);
                    }

                    if (!_cu.rtnHtS(_item["FILE2_"]).Equals(""))
                    {
                        ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_item["FILE2_ORIGIN"]));
                        lvi.SubItems.Add(_cu.rtnHtS(_item["FILE2_"]));
                        lvFileList.Items.Add(lvi);
                    }

                    if (!_cu.rtnHtS(_item["FILE3_"]).Equals(""))
                    {
                        ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_item["FILE3_ORIGIN"]));
                        lvi.SubItems.Add(_cu.rtnHtS(_item["FILE3_"]));
                        lvFileList.Items.Add(lvi);
                    }

                    if (!_cu.rtnHtS(_item["FILE4_"]).Equals(""))
                    {
                        ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_item["FILE4_ORIGIN"]));
                        lvi.SubItems.Add(_cu.rtnHtS(_item["FILE4_"]));
                        lvFileList.Items.Add(lvi);
                    }
                }
            }
        }

        //탭이동시
        private void msgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;

            lb_userNm.Text = "-";
            lb_date.Text = "-";
            messageBody.DocumentText = "";
            lvFileList.Items.Clear();
            lvFileList2.Items.Clear();

            if (current.Text.Equals("받은 메세지"))
            {
                label2.Text = "보낸 사람";
                label1.Text = "받은 일자";
                if(lvRcv.Items.Count > 0)  lvRcv.Items[0].Selected = true;
                lvSend.SelectedIndices.Clear();
                panel3.Visible = true;
                panel4.Visible = false;
            }
            else
            {
                label2.Text = "받은 사람";
                label1.Text = "보낸 일자";
                if (lvSend.Items.Count > 0) lvSend.Items[0].Selected = true;
                lvRcv.SelectedIndices.Clear();
                panel3.Visible = false;
                panel4.Visible = true;
            }          
        }

        private void btn_donwload_Click(object sender, EventArgs e)
        {
            if(lvFileList.Items.Count < 1)
            {
                MessageBox.Show("저장할 파일이 존재하지 않습니다.");
                return;
            }

            FileDownload();
        }

        private void FileDownload()
        {
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(downloadFileFtp);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged_Ftp);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted_Ftp);

            backgroundWorker1.RunWorkerAsync();
        }

        void downloadFileFtp(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            String[,] _downloadFiles = null;

            this.Invoke(new MethodInvoker(delegate ()
            {
                //받은파일
                if (msgList.SelectedTab == msgList.TabPages["tabPage1"])
                {
                    _downloadFiles = new string[lvFileList.Items.Count, 2];

                    for (int i = 0; i < lvFileList.Items.Count; i++)
                    {
                        ListViewItem fItem = lvFileList.Items[i];
                        _downloadFiles[i, 0] = fItem.SubItems[0].Text;
                        _downloadFiles[i, 1] = fItem.SubItems[1].Text;
                    }
                }
                else
                {
                    //보낸파일
                    _downloadFiles = new string[lvFileList2.Items.Count, 2];

                    for (int i = 0; i < lvFileList2.Items.Count; i++)
                    {
                        ListViewItem fItem = lvFileList2.Items[i];
                        _downloadFiles[i, 0] = fItem.SubItems[0].Text;
                        _downloadFiles[i, 1] = fItem.SubItems[1].Text;
                    }
                }
            }));

            ftpFileDownloadProcess(_downloadFiles);
        }

        public void ftpFileDownloadProcess(String[,] _downloadFiles)
        {
            for (int fileCnt = 0; fileCnt < _downloadFiles.GetLength(0); fileCnt++)
            {
                long fws = 0;
                // 파일을 다운로드한다.

                var res = _cu.Connect(_downloadFiles[fileCnt, 1], WebRequestMethods.Ftp.DownloadFile, ref fws, cf.FtpPath, cf.FtpUser, cf.FtpPass);
                FtpWebResponse resp = res.GetResponse() as FtpWebResponse;

                using (var stream = resp.GetResponseStream())
                {
                    // stream을 통해 파일을 작성한다.
                    using (var fs = System.IO.File.Create(cf.LocalDownloadPath + "\\" + _downloadFiles[fileCnt, 0]))
                    {
                        try
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    label4.Text = "다운로드파일 : " + _downloadFiles[fileCnt, 0];
                                }));
                            }

                            byte[] buffer = new byte[10 * 1024 * 1024];
                            int read;
                            long total = 0;
                            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, read);
                                total += read;

                                int percents = (int)(total * 100 / fws);

                                strSpeed = "다운로드 진행률 : " + string.Format("{0:#,##0}kb", total) + " / " + string.Format("{0:#,##0}kb", fws) + "(" + percents + "%)";

                                backgroundWorker1.ReportProgress(percents);
                            }

                            res = null;
                            resp.Dispose();
                            stream.Flush();
                            stream.Close();
                            stream.Dispose();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            res = null;
                            resp.Dispose();
                            throw ex;
                        }
                    }
                }
            }
        }

        void worker_ProgressChanged_Ftp(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label21.Text = strSpeed;
            label22.Text = strLeftTime;
        }

        void worker_RunWorkerCompleted_Ftp(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker1.DoWork -= new DoWorkEventHandler(downloadFileFtp);
            backgroundWorker1.ProgressChanged -= new ProgressChangedEventHandler(worker_ProgressChanged_Ftp);
            backgroundWorker1.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted_Ftp);

            if (e.Error == null)
            {
                download_panel.Visible = false;
                MessageBox.Show("다운로드 되었습니다.");
                Process.Start(MainProg.CConf.LocalDownloadPath);
            }
        }

        //메세지 검색
        private void btn_search_Click(object sender, EventArgs e)
        {
            Hashtable _param = new Hashtable();
            _param.Add("REGI_ID", MainProg.GetUserId());
            _param.Add("MESSAGE_KB", "1");
            _param.Add("SEARCHKB", cbx01.SelectedIndex);
            _param.Add("SEARCHTXT", txt_notice_search.Text.Trim());

            PacketData_MessageSearch req = new PacketData_MessageSearch();
            req.criInfo = _param;
            MainProg.client.Send(req);
        }

        //회신처리
        private void btn_rtn_message_Click(object sender, EventArgs e)
        {
            if(lb_userNm.Text.Length < 3)
            {
                MessageBox.Show("회신할 대상이 존재하지 않습니다.");
                return;
            }

            String rtnName = lb_userNm.Text.IndexOf(";") > -1 ? lb_userNm.Text.Split(';')[0] : lb_userNm.Text;

            String _memberid = "F" + MainProg.getUserInfoByKey("MEMBERNAME", rtnName).MEMBERID;
            //MessageFormBox mfb = new MessageFormBox(_memberid, null, lb_cindex.Text);
            //mfb.Show();

            Hashtable _param = new Hashtable();
            _param.Add("C_INDEX", lb_cindex.Text);
            _param.Add("MESSAGE_KB", "1");

            PacketData_MessageReWrite req = new PacketData_MessageReWrite();
            req.rcvUser = _memberid;
            req.criInfo = _param;
            MainProg.client.Send(req);
        }

        private void btn_donwload2_Click(object sender, EventArgs e)
        {
            if (lvFileList2.Items.Count < 1)
            {
                MessageBox.Show("저장할 파일이 존재하지 않습니다.");
                return;
            }

            FileDownload();
        }
    }
}
