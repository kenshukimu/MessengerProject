using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.FILESYSTEM
{
    public class BinaryFile : IDisposable
    {
        /// <summary>
        /// stream
        /// </summary>
        protected MemoryStream m_stream = new MemoryStream();
        /// <summary>
        /// lock
        /// </summary>
        protected Object m_baseBinaryLock = new Object();

        
		public BinaryFile()
        {
        }
        
        public BinaryFile(BinaryFile b)
        {
            lock (b.m_baseBinaryLock)
            {
                m_stream = b.m_stream;
            }
        }


        /// <summary>
        /// 바이너리를 주어진 파일에 저장
        /// </summary>
        /// <param name="filename">the name of the file to save</param>
        /// <returns>true if successfully saved, otherwise false</returns>
        public bool SaveToFile(String filename)
        {
            lock (m_baseBinaryLock)
            {
                try
                {

                    using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                    {
                        writer.Write(m_stream.ToArray());
                        writer.Flush();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                    return false;
                }
            }
        }

        /// <summary>
        /// 주어진 파일에 바이너리 추가
        /// </summary>
        /// <param name="filename">the name of the file to append</param>
        /// <returns>true if successfully saved, otherwise false</returns>
        public bool AppendToFile(String filename)
        {
            lock (m_baseBinaryLock)
            {
                try
                {

                    using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Append)))
                    {
                        writer.Write(m_stream.ToArray());
                        writer.Flush();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                    return false;
                }
            }
        }


        /// <summary>
        /// 지정된 파일에서 속성 목록로드
        /// </summary>
        /// <param name="filename">the name of the file to load</param>
        /// <returns>true if successfully loaded, otherwise false</returns>
        public bool LoadFromFile(String filename)
        {
            lock (m_baseBinaryLock)
            {
                try
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
                    {
                        FileInfo fInfo = new FileInfo(filename);
                        m_stream.SetLength(fInfo.Length);
                        reader.Read(m_stream.GetBuffer(), 0, (int)fInfo.Length);
                        m_stream.Seek(0, SeekOrigin.Begin);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                    return false;
                }
            }
        }

        /// <summary>
        /// 현재 스트림 가져 오기
        /// </summary>
        /// <returns>the current stream</returns>
        public MemoryStream GetStream()
        {
            lock (m_baseBinaryLock)
            {
                return m_stream;
            }
        }


        /// <summary>
        /// 스트림을 지정된 스트림으로 설정
        /// </summary>
        /// <param name="stream">the stream to set</param>
        public void SetStream(MemoryStream stream)
        {
            lock (m_baseBinaryLock)
            {
                m_stream = stream;
            }
        }

        /// <summary>
        /// 이 인스턴스가 삭제되었는지 여부를 나타내는 값을 가져 오거나 설정
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>Default initialization for a bool is 'false'</remarks>
        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (m_stream != null)
                        {
                            m_stream.Dispose();
                            m_stream = null;
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

        ~BinaryFile() { Dispose(false); }
    }
}
