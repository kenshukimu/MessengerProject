using KSM.DAO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Kyobo_Msg_Client
{
    public partial class NoticeView : Form
    {
        _Sel _sel = new _Sel();
        _Ins _Ins = new _Ins();
        CommonUtil _cu = new CommonUtil();
        String strSpeed = "";
        String strLeftTime = "";
        ClientConfig cf = MainProg.CConf;

        public NoticeView(String noticeNum)
        {
            InitializeComponent();
            
            viewBorderDetail(noticeNum);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void viewBorderDetail(String noticeNum)
        {
            Hashtable _param = new Hashtable();
            _param.Add("C_INDEX", noticeNum);
            _param.Add("MESSAGE_KB", "0");

            _param.Add("ID_READER", MainProg.GetUserId());
            //읽음으로 표시
            _Ins.UpdatetHashData(_param, "mergeNoticeMessageMsgDetail");

            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderList");

            if(_list.Count > 0)
            {
                NoticeTitle.Text = _cu.rtnHtS(_list[0]["TITLE"]);
                NoticeBody.DocumentText = _cu.rtnHtS(_list[0]["CONTEXT"]);

                NoticeName.Text = _cu.rtnHtS(_list[0]["NAME"]);
                NoticeDay.Text = _cu.ToYYYYMMDD(_cu.rtnHtS(_list[0]["REGI_DT"]),true);

                lvFileList.View = View.Details;
                lvFileList.HeaderStyle = ColumnHeaderStyle.None;

                lvFileList.BeginUpdate();

                if(!_cu.rtnHtS(_list[0]["FILE_"]).Equals(""))
                {
                    ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_list[0]["FILE_ORIGIN"]));
                    lvi.SubItems.Add(_cu.rtnHtS(_list[0]["FILE_"]));                    
                    lvFileList.Items.Add(lvi);
                }

                if (!_cu.rtnHtS(_list[0]["FILE1_"]).Equals(""))
                {
                    ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_list[0]["FILE1_ORIGIN"]));
                    lvi.SubItems.Add(_cu.rtnHtS(_list[0]["FILE1_"]));
                    lvFileList.Items.Add(lvi);
                }

                if (!_cu.rtnHtS(_list[0]["FILE2_"]).Equals(""))
                {
                    ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_list[0]["FILE2_ORIGIN"]));
                    lvi.SubItems.Add(_cu.rtnHtS(_list[0]["FILE2_"]));
                    lvFileList.Items.Add(lvi);
                }

                if (!_cu.rtnHtS(_list[0]["FILE3_"]).Equals(""))
                {
                    ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_list[0]["FILE3_ORIGIN"]));
                    lvi.SubItems.Add(_cu.rtnHtS(_list[0]["FILE3_"]));
                    lvFileList.Items.Add(lvi);
                }

                if (!_cu.rtnHtS(_list[0]["FILE4_"]).Equals(""))
                {
                    ListViewItem lvi = new ListViewItem(_cu.rtnHtS(_list[0]["FILE4_ORIGIN"]));
                    lvi.SubItems.Add(_cu.rtnHtS(_list[0]["FILE4_"]));
                    lvFileList.Items.Add(lvi);
                }

                lvFileList.EndUpdate();

            }
        }

        private void btn_getFIle_Click(object sender, EventArgs e)
        {
            if (lvFileList.Items.Count < 1)
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
                MessageBox.Show("다운로드 되었습니다.");
                //if (MessageBox.Show("다운받은 폴더를 여시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                    Process.Start(MainProg.CConf.LocalDownloadPath);
                //}
            }
        }
    }
}
