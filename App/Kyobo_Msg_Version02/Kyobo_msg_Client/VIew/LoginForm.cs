//using KSM.DAO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.ServiceModel;
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
          
            //로그인시 서버에 접속할 수 없으므로 WCF를 이용하여 처리
            ChannelFactory<WcfIService> factory = new ChannelFactory<WcfIService>();
            // Address
            string address = "net.tcp://52.141.56.159:5443/WcfService";
            //string address = "net.tcp://localhost:5443/WcfService";
            factory.Endpoint.Address = new EndpointAddress(address);

            NetTcpBinding netTcpBinding = new NetTcpBinding();
            netTcpBinding.Security.Mode = SecurityMode.None;

            // Binding : TCP 사용
            factory.Endpoint.Binding = netTcpBinding;
            // Contract 설정
            factory.Endpoint.Contract.ContractType = typeof(WcfIService);
            // Channel Factory 만들기
            WcfIService channel = factory.CreateChannel();
            // Server 쪽 함수 호출
            Hashtable _userInfo = channel.LoginCheck(txtUserId.Text.Trim(), hashValue);
            // Close Channel
            ((ICommunicationObject)channel).Close();
            
            String[] login_user_info = new string[12];

           if(_userInfo != null)
           {
                //var user = _list[0];
                login_user_info[0] = txtUserId.Text.Trim();
                login_user_info[1] = _cu.rtnHtS(_userInfo["SWCD"]);
                login_user_info[2] = _cu.rtnHtS(_userInfo["NAME"]);
                login_user_info[3] = _cu.rtnHtS(_userInfo["LEVEL_CD"]);
                login_user_info[4] = _cu.rtnHtS(_userInfo["ADMIN"]);
                login_user_info[5] = _cu.rtnHtS(_userInfo["ORG_CD"]);
                login_user_info[6] = _cu.rtnHtS(_userInfo["GROUP_ID"]);
                login_user_info[7] = _cu.rtnHtS(_userInfo["BADMIN"]);
                login_user_info[8] = txtPass.Text;

                login_user_info[9] = serverIP.Text;
                login_user_info[10] = serverPort.Text;

                login_user_info[11] = txtPass.Text;

                MainProg.ConfigWriteProfile(login_user_info[0], login_user_info[9], login_user_info[10]);

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

        [ServiceContract]
        public interface WcfIService
        {
            [OperationContract]
            Hashtable GetData(int value);

            // TODO: 여기에 서비스 작업을 추가합니다.
            [OperationContract]
            Hashtable LoginCheck(String userId, String password);
        }

        private void btn_AddPerson_Click(object sender, EventArgs e)
        {

        }
    }
}
