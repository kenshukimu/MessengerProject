using KSM.DAO.Models;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Kyobo_Msg_Client
{
    public partial class UserInfo : Form
    {
        CommonUtil _cu = new CommonUtil();
        ClientConfig cf = MainProg.CConf;

        public UserInfo()
        {
            InitializeComponent();
        }

        public UserInfo(UserDetailInfo udll)
        {
            InitializeComponent();
            SetInfo(udll);
        }

        //화면 초기 이벤트
        private void SetInfo(UserDetailInfo udll)
        {
            orgName.Text = udll.GROUPNAME;
            rankName.Text = udll.RANKNAME;
            name.Text = udll.MEMBERNAME;
            email.Text = udll.EMAIL;
            tel.Text = udll.OFFICEPHONE;
            hp.Text = udll.HP;
            
            string _photoPath = MainProg.CConf.FtpPath + "MPhoto/";
            string _fileName = udll.MEMBERID + ".jpg";

            byte[] _imgByte = _cu.GetImgByte(_photoPath, MainProg.CConf.FtpUser, MainProg.CConf.FtpPass, _fileName);
            profileBox.Image = _cu.ByteToImage(_imgByte);
            profileBox.SizeMode = PictureBoxSizeMode.StretchImage;

            if(udll.MEMBERID.Equals(MainProg.GetUserId()))
            {
                btn_savePhoto.Visible = true;
            }
            else
            {
                btn_savePhoto.Visible = false;
            }
        }

        //사진등록 이벤트
        private void btn_savePhoto_Click(object sender, EventArgs e)
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
                long fws = 0;

                FtpWebRequest res = _cu.Connect(MainProg.GetUserId() + ".jpg", WebRequestMethods.Ftp.UploadFile, ref fws, cf.FtpPath + "MPhoto/", cf.FtpUser, cf.FtpPass);
                using (var stream = res.GetRequestStream())
                {
                    using (var fs = System.IO.File.OpenRead(fileFullName))
                    {
                        byte[] buffer = new byte[10 * 1024 * 1024];
                        int read;
                        long total = 0;
                        if (fws == 0)
                        {
                            fws = new FileInfo(fileFullName).Length;
                        }

                        while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            stream.Write(buffer, 0, read);
                            total += read;
                        }
                    }
                }
                MessageBox.Show("저장되었습니다.");
                UserDetailInfo udll = new UserDetailInfo();
                udll.MEMBERID = MainProg.GetUserId();

                udll.GROUPNAME = orgName.Text;
                udll.RANKNAME = rankName.Text;
                udll.MEMBERNAME = name.Text;
                udll.EMAIL = email.Text;
                udll.OFFICEPHONE = tel.Text;
                udll.HP = hp.Text;

                //창에 사진 바꾸기
                SetInfo(udll);
                //메인창에 사진 바꾸기
                ((MainForm)(this.Owner)).SetPhoto(udll.MEMBERID);
            }
        }
    }
}
