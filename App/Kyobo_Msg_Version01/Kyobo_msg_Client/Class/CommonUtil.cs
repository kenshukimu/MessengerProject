using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kyobo_Msg_Client
{
    class CommonUtil
    {
        /// <summary>
        /// 객체의 값을 가져온다.(NULL일 경우 빈 문자열 반환)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string rtnHtS(object data)
        {
            return data == null ? string.Empty : data.ToString();
        }

        public string GetLocalIP()
        {
            string localIP = "Not available, please check your network seetings!";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        /// <summary>
		/// 문자열을 날짜형식 문자열로 변환
		///		<para>YYYYMMDD -> YYYY구분자MM구분자DD</para>
		/// </summary>
		/// <param name="date">날짜 문자열(YYYYMMDD)</param>
		/// <param name="splitChar">구분자</param>
		/// <returns></returns>
		public string ToYYYYMMDD(string date, string splitChar)
        {
            if (date.Length == 8)
            {
                return date.Substring(0, 4) + splitChar + date.Substring(4, 2) + splitChar + date.Substring(6, 2);
            }

            return date;
        }

        public string ToYYYYMMDD(string date, bool korean)
        {
            if (date.Length == 8)
            {
                if (korean)
                {
                    return string.Format("{0}년 {1}월 {2}일", date.Substring(0, 4), date.Substring(4, 2), date.Substring(6, 2));
                }
                else
                {
                    return string.Format("{0}-{1}-{2}", date.Substring(0, 4), date.Substring(4, 2), date.Substring(6, 2));
                }
            }

            return date;
        }

        public Boolean extendtionYN(String _file)
        {
            Boolean _hangul = false;
            char[] charArr = Path.GetExtension(_file).ToCharArray();
            foreach (char c in charArr)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    _hangul = true;
                    break;
                }
            }
            return _hangul;
        }

        public String CreateDirectory(String l_sDirectoryName)
        {
            DirectoryInfo l_dDirInfo = new DirectoryInfo(l_sDirectoryName);
            if (l_dDirInfo.Exists == false)
            {
                Directory.CreateDirectory(l_sDirectoryName);
            }
            return l_sDirectoryName;
        }

        public FtpWebRequest Connect(String url, string method, ref long fws, String ftpPath, String ftpUser, String ftpPass)
        {
            // WebRequest 클래스를 이용해 접속하기 때문에 객체를 가져온다. (FtpWebRequest로 변환)
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath + url);

            // Binary 형식으로 사용한다.
            request.UseBinary = true;

            if (method.Equals(System.Net.WebRequestMethods.Ftp.DownloadFile))
            {
                request.UsePassive = true;
                request.KeepAlive = true;

                //파일사이즈 체크
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);

                fws = request.GetResponse().ContentLength;
                request.GetResponse().Close();

                request = (FtpWebRequest)WebRequest.Create(ftpPath + url);

                //request.Method = WebRequestMethods.Ftp.GetFileSize;
                //request.Credentials = new NetworkCredential(ftpUser, ftpPass);
            }
            else if (method.Equals(System.Net.WebRequestMethods.Ftp.UploadFile))
            {   
                request.UsePassive = false;
                request.KeepAlive = false;

            }else if (method.Equals(System.Net.WebRequestMethods.Ftp.GetFileSize))
            {
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);

                try
                {
                    fws = request.GetResponse().ContentLength;
                    request.GetResponse().Close();
                }
                catch
                {
                    fws = 0;
                }

                return null;
            }
            else
            {
                request.UsePassive = false;
                request.KeepAlive = false;
            }

            // 로그인 인증
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);
            // FTP 메소드 설정(아래에 별도 설명)
            request.Method = method;

            // 접속해서 WebResponse함수를 가져온다.
            return request;
        }

        public byte[] GetImgByte(String ftpPath, String ftpUser, String ftpPass, String url)
        {
            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new NetworkCredential(ftpUser, ftpPass);
            long fileSize = 0;
            byte[] imageByte = null;

            Connect(url, System.Net.WebRequestMethods.Ftp.GetFileSize, ref fileSize, ftpPath, ftpUser, ftpPass);

            if(fileSize > 0)
            {
                imageByte = ftpClient.DownloadData(ftpPath + url);
            }else
            {
                imageByte = ftpClient.DownloadData(ftpPath + "default.jpg");
            }
            return imageByte;
        }

        public Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
    }
}
