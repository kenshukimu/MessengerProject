using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DSDO.COMMON.LIBRARY.Network;
using DSDO.COMMON.UTIL.Network;
using KSM.DAO.Models;

namespace Kyobo_Msg_Client
{
    public partial class MessageFormBox : Form
    {
        CommonUtil _cu = new CommonUtil();
        int boardNo;
        String strSpeed = "";
        String strLeftTime = "";
        
        ClientConfig cf = MainProg.CConf;

        public MessageFormBox()
        {
            InitializeComponent();
        }

        public MessageFormBox(String userId, String selectUserList, String messageNum)
        {
            InitializeComponent();

            txt_rcv_user.BackColor = System.Drawing.Color.White;

            if(selectUserList !=null && selectUserList.Length > 0) {  
                String[] sendUserList = selectUserList.Split('^');

                foreach (String rcvUserid in sendUserList)
                {
                    if (rcvUserid.Equals("") || !rcvUserid.Substring(0,1).Equals("F")) continue;
                    String _memberName=  MainProg.getUserInfoByKey("MEMBERID", rcvUserid.Substring(1)).MEMBERNAME;
                    txt_rcv_user.Text += _memberName + ";";
                }

                if (!sendUserList.Contains(userId))
                {
                    String _memberName = MainProg.getUserInfoByKey("MEMBERID", userId.Substring(1)).MEMBERNAME;
                    txt_rcv_user.Text += _memberName + ";";
                }
            }
            else
            {
                String _memberName = MainProg.getUserInfoByKey("MEMBERID", userId.Substring(1)).MEMBERNAME;
                txt_rcv_user.Text += _memberName + ";";
            }

            if (messageNum == null)
            {
                messageBody.DocumentText = String.Empty;
            }
            else
            {
                Hashtable _param = new Hashtable();
                _param.Add("C_INDEX", messageNum);
                _param.Add("MESSAGE_KB", "1");
                IList<Hashtable> _message = new _Sel().GetListHashData(_param, "getBoarderList");

                foreach (Hashtable _item in _message)
                {  
                    var str = "<html><head></head><body><br/><br/><br/>" + "[" + txt_rcv_user.Text.Replace(";", "") + "님이 보낸글]<br/>";

                    RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
                    Regex regx = new Regex("<body>(?<theBody>.*)</body>", options);

                    Match match = regx.Match(_cu.rtnHtS(_item["CONTEXT"]));
                    string theBody = String.Empty;
                    if (match.Success)
                    {
                       theBody = match.Groups["theBody"].Value;
                    }
                    str += theBody;
                    str += "</ body ></ html > ";

                    messageBody.DocumentText += str;
                }
            }

            lvFileList.View = View.Details;
            lvFileList.Columns.Add("fileName", 600, HorizontalAlignment.Left);
            lvFileList.Columns.Add("fileFullName", 0);

            lvFileList.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MessageFormBox_DragDrop(object sender, DragEventArgs e)
        {
            var dropped = ((string[])e.Data.GetData(DataFormats.FileDrop));
            var files = dropped.ToList();

            if (!files.Any())
                return;

            foreach (string drop in dropped)
                if (Directory.Exists(drop))
                    files.AddRange(Directory.GetFiles(drop, "*.*", SearchOption.AllDirectories));

            lvFileList.Update();

            Random r = new Random();
            String _modifyFileName = "me_" + MainProg.GetUserId() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + r.Next(0, 1000).ToString();

            String[] _fileItem = {
                                         Path.GetFileName(files[0]),
                                         files[0],
                                         _modifyFileName
                                    };
            ListViewItem _itemList = new ListViewItem(_fileItem);
            lvFileList.Items.Add(_itemList);

            lvFileList.EndUpdate();
        }

        private void MessageFormBox_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop, false)==true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        //메세지 보내기
        private void btn_Send_Click(object sender, EventArgs e)
        {
            if(txt_rcv_user.Text.Length < 1)
            {
                MessageBox.Show("받을사람이 지정되어 있지 않습니다.");
                return;
            }

            //테이블 등록처리
            _Ins _ins = new _Ins();
            Hashtable _param = new Hashtable();
            _param.Add("TITLE", "메세지");

            _param.Add("CONTEXT", messageBody.DocumentText);
            _param.Add("REGI_ID", MainProg.GetUserId());

            _param.Add("MESSAGE_KB", "1");            

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
            _param.Add("DISPLAY_DT", "99991231");
            
            //추가일 경우
            boardNo = _ins.InsertHashDataBySelectKey(_param, "insertBoard");

            FileUpload();
        }

        private void btn_addFile_Click(object sender, EventArgs e)
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
                String _modifyFileName = "me_" + MainProg.GetUserId() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + r.Next(0, 1000).ToString();

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

                //받은사람에게 전송처리            
                PacketData_SendMessage req = new PacketData_SendMessage();
                req.userId = MainProg.GetUserId();
                req.boardNo = boardNo;

                Hashtable _param = new Hashtable();
                List<String> _rcvvUserId = new List<string>();
                _param.Add("C_INDEX", boardNo);

                foreach (String userNm in txt_rcv_user.Text.Split(';'))
                {
                    if (userNm.Equals("")) continue;
                    String _memberId = MainProg.getUserInfoByKey("MEMBERNAME", userNm).MEMBERID;

                    _rcvvUserId.Add(_memberId);
                    _param.Remove("ID_READER");

                    //읽은 사람 등록
                    _param.Add("ID_READER", _memberId);
                    new _Ins().InsertHashData(_param, "insertBoardDetail");
                }

                MessageBox.Show("전송 되었습니다.");

                req.revUserId = _rcvvUserId;
                MainProg.client.Send(req);
                this.Close();
            }
        }

        public void setRcvUsers(String userNameList)
        {
            txt_rcv_user.Text = userNameList;
        }

        private void txt_rcv_user_MouseClick_1(object sender, MouseEventArgs e)
        {
            findUser fu = new findUser(txt_rcv_user.Text);
            fu.Owner = this;
            fu.ShowDialog();
        }
    }
}
