using KSM.DAO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Kyobo_Msg_Client
{
    public partial class LoginForm : Form
    {
        CommonUtil _cu = new CommonUtil();
        public LoginForm()
        {
            InitializeComponent();
            ClientConfig cf = MainProg.CConf;

            txtUserId.Text = cf.userId;
            serverIP.Text = cf.serverIP;
            serverPort.Text = cf.serverPORT.ToString();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            login_process();
        }

        private void login_process()
        {
            if (txtPass.Text == null || txtPass.Text == "")
            {
                MessageBox.Show("패스워드를 입력하세요.");
                return;
            }

            HashAlgorithm hash = new SHA256Managed();
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(txtPass.Text);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);

            //in this string you got the encrypted password
            string hashValue = Convert.ToBase64String(hashBytes);

            Hashtable _param = new Hashtable();

            _param.Add("MGS_USERID", txtUserId.Text.Trim());
            _param.Add("PASSWD", hashValue);

            _Sel _sel = new _Sel();
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getLoginUserInfo");
            String[] login_user_info = new string[12];

           if(_list.Count > 0)
           {
                var user = _list[0];
                login_user_info[0] = txtUserId.Text.Trim();
                login_user_info[1] = _cu.rtnHtS(user["SWCD"]);
                login_user_info[2] = _cu.rtnHtS(user["NAME"]);
                login_user_info[3] = _cu.rtnHtS(user["LEVEL_CD"]);
                login_user_info[4] = _cu.rtnHtS(user["ADMIN"]);
                login_user_info[5] = _cu.rtnHtS(user["ORG_CD"]);
                login_user_info[6] = _cu.rtnHtS(user["GROUP_ID"]);
                login_user_info[7] = _cu.rtnHtS(user["BADMIN"]);
                login_user_info[8] = txtPass.Text;

                login_user_info[9] = serverIP.Text;
                login_user_info[10] = serverPort.Text;

                login_user_info[11] = txtPass.Text;

                MainProg.Instance.ConfigWriteProfile(login_user_info[0], login_user_info[9], login_user_info[10]);

                MainProg.CConf.userId = login_user_info[0];

                this.Visible = false;
                MainForm mf = new MainForm(login_user_info);
                mf.Show();
            }
            else
            {
                MessageBox.Show("패스워드를 다시 입력하세요.");
                return;
            }
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { login_process(); }
        }
    }
}
