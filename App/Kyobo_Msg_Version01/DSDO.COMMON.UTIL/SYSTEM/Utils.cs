using DSDO.COMMON.UTIL.FILESYSTEM;
using System;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;

namespace DSDO.COMMON.UTIL.SYSTEM
{
    public class Utils
    {


        /// <summary>
        /// CPU Core 값 
        /// </summary>
        /// <returns>CPU Core 값 </returns>
        public static int GetCoresNumber()
        {
            int iCore = 0;

            ManagementObjectSearcher mos = new ManagementObjectSearcher();
            mos.Query.QueryString = "SELECT * FROM Win32_Processor";


            foreach (var item in mos.Get())
            {
                iCore += int.Parse(item["NumberOfCores"].ToString());
            }

            return iCore;
        }

        /// <summary>
        /// String to Byte 로 변환
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns>Bytes</returns>
        public static byte[] GetBytes(string strValue)
        {
            byte[] bytes = new byte[strValue.Length * sizeof(char)];
            System.Buffer.BlockCopy(strValue.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }


        /// <summary>
        /// Byte to String 으로 변환
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>String</returns>
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// 실행파일 위치폴더값 리턴
        /// </summary>
        /// <returns>실행파일 위치폴더값</returns>
        public static string GetApplicationPath()
        {
            return FolderHelper.GetModuleFileDirectory();
        }

        public static string GetLocalIP()
        {
            string myIP = String.Empty;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myIP = ip.ToString();
                    break;
                }
            }
            return myIP;
        }

        public static string GetAddSecondFileName(string fname)
        {
            return Path.GetFileNameWithoutExtension(fname) + "_" + DateTimeHelper.GetNowTime() + Path.GetExtension(fname);
        }
    }
}
