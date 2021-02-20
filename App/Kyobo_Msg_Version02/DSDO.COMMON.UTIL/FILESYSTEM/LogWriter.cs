using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.FILESYSTEM
{
    public sealed class LogWriter : BaseTextFile
    {
        /// <summary>
        /// Log File 명
        /// </summary>
        private String m_fileName;

        /// <summary>
        /// Log 값
        /// </summary>
        private StringBuilder m_logString;

        /// <summary>
        /// lock
        /// </summary>
        private Object m_logLock = new Object();

        /// <summary>
        /// 현재 시간 작성
        /// </summary>
        /// <param name="pMsg">the message to print to the log file.</param>
        public void WriteLog(String pMsg)
        {
            lock (m_logLock)
            {
                DateTime curTime = DateTime.Now;
                m_logString = new StringBuilder();
                m_logString.AppendFormat("{0}/{1}/{2}, {3}:{4}:{5}.{6}  :  {7}\n", curTime.Month, curTime.Day, curTime.Year, curTime.Hour, curTime.Minute, curTime.Second, curTime.Millisecond, pMsg);
                AppendToFile(m_fileName);
            }
        }


        public LogWriter()
        {
            m_fileName = FolderHelper.GetModuleFileName();
            m_fileName = m_fileName.Replace(".EXE", ".log");
            m_fileName = m_fileName.Replace(".exe", ".log");


        }


        public LogWriter(string logFilename = null)
        {
            if (logFilename == null)
            {
                m_fileName = FolderHelper.GetModuleFileName();
                m_fileName = m_fileName.Replace(".EXE", ".log");
                m_fileName = m_fileName.Replace(".exe", ".log");
            }
            else
                m_fileName = logFilename;

        }


        public LogWriter(LogWriter b)
        {
            m_fileName = b.m_fileName;
        }

        /// <summary>
        /// 파일에 쓰는  Loop 함수
        /// </summary>
        protected override void WriteLoop()
        {
            WriteToFile(m_logString.ToString());
        }


        /// <summary>
        /// 실제로드 파일에서 값을로드하는 기능
        /// </summary>
        /// <param name="stream">stream from the file</param>
        protected override void LoadFromFile(StreamReader stream)
        {
        }




    }
}
