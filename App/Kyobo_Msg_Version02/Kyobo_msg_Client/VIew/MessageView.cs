using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DSDO.COMMON.LIBRARY.Network;
using DSDO.COMMON.UTIL.Network;
//using KSM.DAO.Models;

namespace Kyobo_Msg_Client
{
    public partial class MessageView : Form
    {
        ClientConfig cf = MainProg.CConf;
        //_Sel _sel = new _Sel();
        CommonUtil _cu = new CommonUtil();
        String strSpeed = "";
        String strLeftTime = "";
        int _messageNo;

        //작업표시줄 반짝거리기
        public const int FLASHW_STOP = 0;
        public const int FLASHW_ALL = 3;

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public int cbSize;
            public IntPtr hwnd;
            public int dwFlags;
            public int uCount;
            public int dwTimeout;
        }

        [DllImport("user32.dll")]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Flash(true);
        }

        //창 초기화
        public MessageView(Hashtable messageDetail, String userId, int messageNo)
        {
            InitializeComponent();
            this.Text = "메세지 보기";

            //회신할때 사용
            _messageNo = messageNo;

            lvFileList.View = View.Details;
            lvFileList.Columns.Add("fileName", 600, HorizontalAlignment.Left);
            lvFileList.Columns.Add("fileFullName", 0);

            lvFileList.HeaderStyle = ColumnHeaderStyle.None;

            //창 열릴때 본 시간 저장
            Hashtable _param = new Hashtable();
            _param.Add("C_INDEX", messageNo);
            _param.Add("ID_READER", MainProg.GetUserId());
            //new _Ins().UpdatetHashData(_param, "updateBoardDetail");

            PacketData_SendMessage_Read req = new PacketData_SendMessage_Read();
            req.criInfo = _param;
            MainProg.client.Send(req);

            //상세화면 표시
            viewMessageDetail(messageDetail);
        }      

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_downFile_Click(object sender, EventArgs e)
        {
            if (lvFileList.Items.Count < 1)
            {
                MessageBox.Show("저장할 파일이 존재하지 않습니다.");
                return;
            }

            FileDownload();
        }

        private void viewMessageDetail(Hashtable _item)
        {
            //Hashtable _param = new Hashtable();
            //_param.Add("C_INDEX", messageNum);
            //_param.Add("MESSAGE_KB", "1");
            //IList<Hashtable> _message = _sel.GetListHashData(_param, "getBoarderList");

            //foreach (Hashtable _item in _messageDeatil)
            if(_item != null)
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

                lb_userNm.Text = MainProg.getUserInfoByKey("MEMBERID", _cu.rtnHtS(_item["REGI_ID"])).MEMBERNAME;
                lb_date.Text = _cu.ToYYYYMMDD(_cu.rtnHtS(_item["REGI_DT"]), true);
                messageBody.DocumentText = _cu.rtnHtS(_item["CONTEXT"]);

                lvFileList.View = View.Details;
                lvFileList.HeaderStyle = ColumnHeaderStyle.None;

                lvFileList.BeginUpdate();

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

                lvFileList.EndUpdate();
            }
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
            String[,] _downloadFiles = new string[lvFileList.Items.Count, 2];

            this.Invoke(new MethodInvoker(delegate ()
            {
                for (int i = 0; i < lvFileList.Items.Count; i++)
                {
                    ListViewItem fItem = lvFileList.Items[i];
                    //_upladFIles.Add(fItem.SubItems[1].Text);
                    _downloadFiles[i, 0] = fItem.SubItems[0].Text;
                    _downloadFiles[i, 1] = fItem.SubItems[1].Text;
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
                //MessageBox.Show("다운로드 되었습니다.");
                Process.Start(MainProg.CConf.LocalDownloadPath);
            }
        }

        private void Flash(bool flashed)
        {
            FLASHWINFO fi = new FLASHWINFO();
            fi.cbSize = Marshal.SizeOf(typeof(FLASHWINFO));
            fi.hwnd = this.Handle;
            fi.dwFlags = flashed ? FLASHW_ALL : FLASHW_STOP;
            fi.uCount = 10;
            fi.dwTimeout = 500;

            FlashWindowEx(ref fi);
        }

        private void MessageView_MouseClick(object sender, MouseEventArgs e)
        {
            Flash(false);
        }

        private void btn_rtn_message_Click(object sender, EventArgs e)
        {
            String rtnName = lb_userNm.Text.IndexOf(";") > -1 ? lb_userNm.Text.Split(';')[0] : lb_userNm.Text;

            String _memberid ="F" + MainProg.getUserInfoByKey("MEMBERNAME", rtnName).MEMBERID;
            //MessageFormBox mfb = new MessageFormBox(_memberid, null, _messageNo);
            //mfb.Show();
            Hashtable _param = new Hashtable();
            _param.Add("C_INDEX", _messageNo);
            _param.Add("MESSAGE_KB", "1");

            PacketData_MessageReWrite req = new PacketData_MessageReWrite();
            req.rcvUser = _memberid;
            req.criInfo = _param;
            MainProg.client.Send(req);
        }
    }
}
