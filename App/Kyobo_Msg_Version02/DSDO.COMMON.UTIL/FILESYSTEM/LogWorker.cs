using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using DSDO.COMMON.UTIL.LOCK;
using System.IO;

namespace DSDO.COMMON.UTIL.FILESYSTEM
{
    public class LogWorker : BaseTextFile, IDisposable
    {
        /// <summary>
        /// Stop event
        /// </summary>
        EventEx m_threadStopEvent = new EventEx(false, EventResetMode.ManualReset);
        /// <summary>
        /// Lock for log
        /// </summary>
        Object m_logLock = new Object();
        /// <summary>
        /// Lock for Thread
        /// </summary>
        Object m_threadLock = new Object();
        /// <summary>
        ///  log file 명
        /// </summary>
        string m_fileName;
        /// <summary>
        ///  Log 값
        /// </summary>
        string m_logString;
        /// <summary>
        ///  Log Queue
        /// </summary>
        Queue<string> m_logQueue = new Queue<string>();
        /// <summary>
        /// Thread
        /// </summary>
        ThreadEx m_thread;

        /// <summary>
        /// log file 명
        /// </summary>
        public String FileName
        {
            get
            {
                return m_fileName;
            }
        }


        public LogWorker(string fileName, Encoding encodingType = null)
            : base(encodingType)
        {
            m_fileName = fileName;
            m_thread = new ThreadEx(this.Execute, ThreadPriority.Normal);
            m_thread.Start();
        }

        

        ~LogWorker()
        {
            Dispose(false);
        }

        /// <summary>
        /// logging
        /// </summary>
        public void Stop()
        {
            stop();
        }


        public LogWorker(LogWorker b)
            : base(b)
        {
            m_fileName = b.FileName;
            m_thread.Start();
        }

        /// <summary>
        /// log worker 시작
        /// </summary>
        void Execute()
        {
            while (!m_threadStopEvent.WaitForEvent(0))
            {
                Thread.Sleep(1);

                m_logString = "";


                lock (m_logLock)
                {
                    while (m_logQueue.Count != 0)
                    {
                        string logString = m_logQueue.Peek();
                        m_logQueue.Dequeue();

                        DateTime curTime = DateTime.Now;
                        m_logString += "[" + curTime.ToString("yyyy-MM-dd HH:mm:ss-fff") + "] : " + logString + "\r\n";
                    }
                }

                if (m_logString.Length > 0)
                { 
                    AppendToFile(m_fileName);
                }
            }
        }
        /// <summary>
        /// log worker 정지
        /// </summary>
        void stop()
        {
            if (m_thread.GetStatus() != ThreadStatus.TERMINATED)
            {
                m_threadStopEvent.SetEvent();
                m_thread.WaitFor();
            }
        }

        /// <summary>
        /// 현재시간 로그에 메시지 작성
        /// </summary>
        /// <param name="pMsg">the message to print to the log file.</param>
        public void WriteLog(string pMsg)
        {
            // write error or other information into log file
            lock (m_logLock)
            {
                m_logQueue.Enqueue(pMsg);
            }
        }
        /// <summary>
        /// 파일에 쓰는 Loop Function 
        /// </summary>
        protected override void WriteLoop()
        {
            WriteToFile(m_logString);
        }

        /// <summary>
        /// 파일에서 값을로드하는 기능
        /// </summary>
        /// <param name="stream"></param>
        protected override void LoadFromFile(StreamReader stream)
        {
        }


        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        private void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    stop();
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (m_threadStopEvent != null)
                        {
                            m_threadStopEvent.Dispose();
                            m_threadStopEvent = null;
                        }
                    }

                    // Free any unmanaged objects here.
                }
            }
            finally
            {
                this.IsDisposed = true;
            }
        }

    }
}
