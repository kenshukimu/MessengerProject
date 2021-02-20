using DSDO.COMMON.LIBRARY.Network;
using DSDO.COMMON.UTIL.Network;
using KSM.DAO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyobo_Msg_Client
{
    public partial class NoticeWrite : Form
    {
        public delegate void Sub_DataPushEventHandler(Boolean initFg);
        public event Sub_DataPushEventHandler DataPushEvent_Sub;

        String strSpeed = "";
        String strLeftTime = "";

        CommonUtil _cu = new CommonUtil();
        _Ins _ins = new _Ins();
        _Sel _sel = new _Sel();
        int boardNo;

        ClientConfig cf = MainProg.CConf;

        public NoticeWrite(String boardNo)
        {
            InitializeComponent();

            string[] nameArray = null;

            nameArray = new string[MainProg.CConf.userList.Count];

            for (int i = 0; i < MainProg.CConf.userList.Count; i++)
            {
                nameArray[i] = MainProg.CConf.userList[i].MEMBERNAME;
            }

            //파일리스트
            lvFileList.View = View.Details;
            lvFileList.Columns.Add("fileName", 600, HorizontalAlignment.Left);
            lvFileList.Columns.Add("fileFullName", 0);
            lvFileList.Columns.Add("modifyFileName", 0);

            lvFileList.HeaderStyle = ColumnHeaderStyle.None;

           
            if(boardNo != null)
            {
                viewBorderDetail(boardNo);
                BoardNo.Text = boardNo;
                btn_openFiile.Visible = false;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Snd_msg_Click(object sender, EventArgs e)
        {
            if(txt_NoticeTItle.Text.Equals(""))
            {
                MessageBox.Show("제목이 입력되어 있지 않습니다.");
                return;
            }

            Hashtable _param = new Hashtable();
            _param.Add("TITLE", txt_NoticeTItle.Text);

            _param.Add("CONTEXT", NoticeBody.DocumentText);
            _param.Add("REGI_ID", MainProg.GetUserId());

            _param.Add("MESSAGE_KB", "0");


            for (int i = 0; i < lvFileList.Items.Count; i++)
            {
                ListViewItem fItem = lvFileList.Items[i];                
                switch (i)
                {
                    case 0:
                        _param.Add("FILE_", fItem.SubItems[2].Text);
                        _param.Add("FILE_ORIGIN", fItem.SubItems[0].Text);
                        break;
                    case 1:
                        _param.Add("FILE1_", fItem.SubItems[2].Text);
                        _param.Add("FILE1_ORIGIN", fItem.SubItems[0].Text);
                        break;
                    case 2:
                        _param.Add("FILE2_", fItem.SubItems[2].Text);
                        _param.Add("FILE2_ORIGIN", fItem.SubItems[0].Text);
                        break;
                    case 3:
                        _param.Add("FILE3_", fItem.SubItems[2].Text);
                        _param.Add("FILE3_ORIGIN", fItem.SubItems[0].Text);
                        break;
                    case 4:
                        _param.Add("FILE3_", fItem.SubItems[2].Text);
                        _param.Add("FILE4_", fItem.SubItems[0].Text);
                        break;
                }
            }

            if(chkLimit.Checked)
            {
                DateTime dt = limitDate.Value;
                _param.Add("DISPLAY_DT", dt.Year.ToString() + (dt.Month<10?"0" + dt.Month.ToString():dt.Month.ToString()) + (dt.Day < 10 ? "0" + dt.Day.ToString() : dt.Day.ToString()));
            }
            else
            {
                _param.Add("DISPLAY_DT", "99991231");
            }

            //공지사항 번호가 있으면 수정, 없으면 등록
            if(BoardNo.Text != "")
            {
                _param.Add("C_INDEX", BoardNo.Text);
                //변경일 경우
                _ins.UpdatetHashData(_param, "updateBoard");

                boardNo = Int32.Parse(BoardNo.Text);

                MessageBox.Show("수정 되었습니다.");

                PacketData_SendNotice req = new PacketData_SendNotice();
                req.userId = MainProg.GetUserId();
                req.boardNo = boardNo;
                MainProg.client.Send(req);

                this.DataPushEvent_Sub(false);
                this.Dispose();
            }
            else
            {
                download_panel.Visible = true;
                //추가일 경우
                boardNo = _ins.InsertHashDataBySelectKey(_param, "insertBoard");
                FileUpload();
            }     
        }

        private void btn_openFiile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //File명과 확장자를 가지고 온다.
                string fileName = ofd.SafeFileName;
                //File경로와 File명을 모두 가지고 온다.
                string fileFullName = ofd.FileName;
                //File경로만 가지고 온다.
                string filePath = fileFullName.Replace(fileName, "");

                Random r = new Random();
                String _modifyFileName = "bd_" + MainProg.GetUserId() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" +r.Next(0, 1000).ToString();

                lvFileList.Update();

                String[] _fileItem = {
                                         fileName,
                                         fileFullName,
                                         _modifyFileName
                                    };
                ListViewItem _itemList = new ListViewItem(_fileItem);
                lvFileList.Items.Add(_itemList);

                lvFileList.EndUpdate();
            }
        }

        private void FileUpload()
        {   
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(uploadFileFtp);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged_Ftp);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted_Ftp);

            backgroundWorker1.RunWorkerAsync();
        }

        void uploadFileFtp(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            String[,] _uploadFiles = new string[lvFileList.Items.Count, 2];

            this.Invoke(new MethodInvoker(delegate ()
            {
                for (int i = 0; i < lvFileList.Items.Count; i++)
                {
                    ListViewItem fItem = lvFileList.Items[i];
                    _uploadFiles[i, 0] = fItem.SubItems[1].Text;
                    _uploadFiles[i, 1] = fItem.SubItems[2].Text;
                }
            }));

            ftpFileUploadProcess(_uploadFiles);
        }

        public void ftpFileUploadProcess(String[,] _upladFIles)
        {  
            for (int fileCnt=0; fileCnt < _upladFIles.GetLength(0); fileCnt++)
            {
                String[] filePath = _upladFIles[fileCnt,0].Split('\\');
                String file = filePath[filePath.Length - 1];

                Boolean _hangul = _cu.extendtionYN(file);

                if (Path.GetExtension(file) == null || Path.GetExtension(file).Equals("") || _hangul)
                {
                    continue;
                }

                long fws = 0;
                // 파일을 다운로드한다.

                FtpWebRequest res = _cu.Connect(_upladFIles[fileCnt, 1], WebRequestMethods.Ftp.UploadFile, ref fws, cf.FtpPath, cf.FtpUser, cf.FtpPass);
                using (var stream = res.GetRequestStream())
                {
                    // stream을 통해 파일을 작성한다.
                    using (var fs = System.IO.File.OpenRead(_upladFIles[fileCnt, 0]))
                    {
                        try
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    label4.Text = "업로드파일 : " + file;
                                }));
                            }

                            byte[] buffer = new byte[10 * 1024 * 1024];
                            int read;
                            long total = 0;
                            if (fws == 0)
                            {
                                fws = new FileInfo(_upladFIles[fileCnt, 0]).Length;
                            }

                            while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream.Write(buffer, 0, read);
                                total += read;

                                int percents = (int)(total * 100 / fws);

                                strSpeed = "업로드 진행률 : " + string.Format("{0:#,##0}kb", total) + " / " + string.Format("{0:#,##0}kb", fws) + "(" + percents + "%)";

                                backgroundWorker1.ReportProgress(percents);
                            }

                            res = null;
                            stream.Flush();
                            stream.Close();
                            stream.Dispose();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            res = null;
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
            backgroundWorker1.DoWork -= new DoWorkEventHandler(uploadFileFtp);
            backgroundWorker1.ProgressChanged -= new ProgressChangedEventHandler(worker_ProgressChanged_Ftp);
            backgroundWorker1.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted_Ftp);

            if (e.Error == null)
            {
                download_panel.Visible = false;
                MessageBox.Show("등록 되었습니다.");

                PacketData_SendNotice req = new PacketData_SendNotice();
                req.userId = MainProg.GetUserId();
                req.boardNo = boardNo;
                MainProg.client.Send(req);
                this.DataPushEvent_Sub(false);
                this.Dispose();
            }
        }

        private void viewBorderDetail(String noticeNum)
        {
            Hashtable _param = new Hashtable();
            _param.Add("C_INDEX", noticeNum);
            _param.Add("MESSAGE_KB", "0");
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderList");

            if (_list.Count > 0)
            {
                txt_NoticeTItle.Text = _cu.rtnHtS(_list[0]["TITLE"]);
                NoticeBody.DocumentText = _cu.rtnHtS(_list[0]["CONTEXT"]);
               
                lvFileList.View = View.Details;
                lvFileList.HeaderStyle = ColumnHeaderStyle.None;

                lvFileList.BeginUpdate();

                if (!_cu.rtnHtS(_list[0]["FILE_"]).Equals(""))
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
    }
}
