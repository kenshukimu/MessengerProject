using KSM.DAO.Models;
using System.Collections.Generic;

namespace Kyobo_Msg_Client
{
    public class ClientConfig
    {
        public string userId; //유저ID
        public int serverPORT;  //포트번호
        public string serverIP; //서버IP
        public string FtpPath;
        public string FtpUser;
        public string FtpPass;
        public string LocalDownloadPath;

        public List<UserDetailInfo> userList;
    }
}
