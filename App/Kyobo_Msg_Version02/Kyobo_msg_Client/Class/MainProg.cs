using System;
using System.Runtime.InteropServices;
using System.Text;
//using KSM.DAO.Models;
using System.Linq;
using DSDO.COMMON.LIBRARY.Network;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace Kyobo_Msg_Client
{
    public enum CState : int
    {
        State_Init = 0,
        State_Connect,
        State_UserInfoOK,
        State_LoginOK,
        State_Missing,
        //public const uint State_Wait = 0x12;        
    }

    public sealed class MainProg
    {
        public static MainProg Instance { get; } = new MainProg();
        static ClientConfig cConf = new ClientConfig();

        public static ClientConfig CConf { get { return cConf; } set => cConf = value; }

        public static Client client { get; set; }

        public static String GetUserId() { return cConf.userId; }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        CommonUtil _cu = new CommonUtil();

        MainProg() { }

        LoginForm loginForm;
        public static LoginForm GetLoginForm() { return Instance.loginForm; }

        public void Init()
        {
            ConfigGetProfile();
            //getUserLIst();
            _cu.CreateDirectory(cConf.LocalDownloadPath);
            loginForm = new LoginForm();
            Console.WriteLine("MainProgram Init Client");

            client = new Client();
        }

        public static void ConfigGetProfile()
        {
            //Read Config(권한문제로 현디렉토리를 이용할 수는 없음)
            //String IniFilePath = Environment.CurrentDirectory + @"\Config.ini";

            String IniFilePath =  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kico\Config.ini";
            
            if (!File.Exists(IniFilePath))
            {
                ConfigWriteProfile("", "0.0.0.0", "13000");
            }
            
            StringBuilder ret = new StringBuilder();
            GetPrivateProfileString("CONFIG", "UserId", "-", ret, 12, IniFilePath);
            cConf.userId = ret.ToString();
            GetPrivateProfileString("CONFIG", "ServerPORT", "13000", ret, 10, IniFilePath);
            cConf.serverPORT = int.Parse(ret.ToString());
            GetPrivateProfileString("CONFIG", "ServerIP", "0", ret, 16, IniFilePath);
            cConf.serverIP = ret.ToString();
            GetPrivateProfileString("CONFIG", "FtpPath", "0", ret, 50, IniFilePath);
            cConf.FtpPath = ret.ToString();
            GetPrivateProfileString("CONFIG", "FtpUser", "0", ret, 50, IniFilePath);
            cConf.FtpUser = ret.ToString();
            GetPrivateProfileString("CONFIG", "FtpPass", "0", ret, 50, IniFilePath);
            cConf.FtpPass = ret.ToString();
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Kico\\Received Files";
            GetPrivateProfileString("CONFIG", "LocalDownloadPath", path, ret, 300, IniFilePath);

           if(!ret.ToString().Equals(path))
            {
                cConf.LocalDownloadPath = path;
            }
            else
            {
                cConf.LocalDownloadPath = ret.ToString().Equals("") ? path : ret.ToString();
            }
        }

        public static void ConfigWriteProfile(String userID, String ip, String port)
        {
            //Read Config(권한문제로 현디렉토리를 이용할 수는 없음)
            //String IniFilePath = Environment.CurrentDirectory + @"\Config.ini";
            String IniFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kico\Config.ini";

            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Kico\\Received Files";

            WritePrivateProfileString("CONFIG", "UserId", userID, IniFilePath);
            WritePrivateProfileString("CONFIG", "ServerPORT", "13000", IniFilePath);
            WritePrivateProfileString("CONFIG", "ServerIP", ip, IniFilePath);

            //FTP설정            
            WritePrivateProfileString("CONFIG", "FtpPath", Properties.Resources.FtpPath, IniFilePath);
            WritePrivateProfileString("CONFIG", "FtpUser", Properties.Resources.FtpUser, IniFilePath);
            WritePrivateProfileString("CONFIG", "FtpPass", Properties.Resources.FtpPass, IniFilePath);
            
            //다운로드 받을 폴더지정
            WritePrivateProfileString("CONFIG", "LocalDownloadPath", path, IniFilePath);
        }

        public void getUserLIst(IList<Hashtable> _list)
        {
            cConf.userList = _list
                       .Select(s => new UserDetailInfo()
                       {
                           K_MEMBERID = _cu.rtnHtS(s["K_MEMBERID"]),
                           MEMBERID = _cu.rtnHtS(s["MEMBERID"]),
                           GROUPNAME = _cu.rtnHtS(s["GROUPNAME"]),
                           RANKNAME = _cu.rtnHtS(s["RANKNAME"]),
                           MEMBERNAME = _cu.rtnHtS(s["MEMBERNAME"]),
                           EMAIL = _cu.rtnHtS(s["EMAIL"]),
                           OFFICEPHONE = _cu.rtnHtS(s["OFFICEPHONE"]),
                           HP = _cu.rtnHtS(s["HP"])
                       }).ToList<UserDetailInfo>();
        }

        public static UserDetailInfo getUserInfoByKey(String keyName, String key)
        {
            UserDetailInfo uie = new UserDetailInfo();
            try
            {   
                if (keyName.Equals("K_MEMBERID"))
                {
                    uie = cConf.userList.Where(w => w.K_MEMBERID.Equals(key)).Single();
                }
                else if (keyName.Equals("MEMBERID"))
                {
                    uie = cConf.userList.Where(w => w.MEMBERID.Equals(key)).Single();
                }
                else if (keyName.Equals("MEMBERNAME"))
                {
                    uie = cConf.userList.Where(w => w.MEMBERNAME.Equals(key)).Single();
                }
            }
            catch{}
            return uie;
        }

    }
}
