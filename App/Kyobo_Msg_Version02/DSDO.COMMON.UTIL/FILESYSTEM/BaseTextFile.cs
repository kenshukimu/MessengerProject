using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.FILESYSTEM
{
    public abstract class BaseTextFile
    {
        /// <summary>
        /// reader stream
        /// </summary>
        protected StreamReader m_reader = null;
        /// <summary>
        ///  writer stream
        /// </summary>
        protected StreamWriter m_writer = null;
        /// <summary>
        /// Encoding
        /// </summary>
        protected Encoding m_encoding = Encoding.Unicode;
        /// <summary>
        /// lock
        /// </summary>
        protected Object m_baseTextLock = new Object();

        
		public BaseTextFile(Encoding encoding = null)
        {
            if (encoding != null)
            {
                m_encoding = encoding;
            }
        }
        
        public BaseTextFile(BaseTextFile b)
        {
            lock (b.m_baseTextLock)
            {
                m_encoding = b.m_encoding;
                m_reader = b.m_reader;
                m_writer = b.m_writer;
            }
        }

        /// <summary>
        /// 현재 인코딩 가져 오기
        /// </summary>
        /// <returns>current encoding</returns>
        public Encoding GetEncoding()
        {
            lock (m_baseTextLock)
            {
                return m_encoding;
            }
        }
        /// <summary>
        /// 지정된 인코딩으로 인코딩 설정
        /// </summary>
        /// <param name="encoding">encoding to set</param>
        public void SetEncoding(Encoding encoding)
        {
            lock (m_baseTextLock)
            {
                m_encoding = encoding;
            }
        }

        /// <summary>
        /// 주어진 파일에 텍스트를 저장
        /// </summary>
        /// <param name="filename">the name of the file to save</param>
        /// <returns>true if successfully saved, otherwise false</returns>
        public bool SaveToFile(String filename)
        {
            lock (m_baseTextLock)
            {
                try
                {

                    using (m_writer = new StreamWriter(filename, false, m_encoding))
                    {
                        WriteLoop();
                        m_writer.Flush();
                        m_writer.Close();
                        m_writer = null;
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                    return false;
                }
            }

        }


        /// <summary>
        /// 지정된 파일의 텍스트를 추가
        /// </summary>
        /// <param name="filename">the name of the file to append</param>
        /// <returns>true if successfully saved, otherwise false</returns>
        public bool AppendToFile(String filename)
        {
            lock (m_baseTextLock)
            {
                try
                {

                    using (m_writer = new StreamWriter(filename, true, m_encoding))
                    {
                        WriteLoop();
                        m_writer.Flush();
                        m_writer.Close();
                        m_writer = null;
                        return true;

                    }
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
            lock (m_baseTextLock)
            {
                try
                {
                    using (m_reader = new StreamReader(filename, m_encoding, true))
                    {
                        LoadFromFile(m_reader);
                        m_reader.Close();
                        m_reader = null;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                    return false;
                }
            }
        }

        /// <summary>
        /// 지정된 string 값을 파일에 기입
        /// </summary>
        /// <param name="toFileString">the string to write to the file</param>
        protected void WriteToFile(String toFileString)
        {
            lock (m_baseTextLock)
            {
                try
                {

                    if (m_writer != null)
                    {
                        m_writer.Write(toFileString);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                }
            }
        }


        /// <summary>
        /// 파일에 쓰는  Loop 함수
        /// </summary>
        /// <remarks>Sub classes should implement this function</remarks>
		protected abstract void WriteLoop();

        /// <summary>
        /// 실제로드 파일에서 값을로드하는 기능
        /// </summary>
        /// <param name="stream">stream from the file</param>
        /// <remarks>Sub classes should implement this function</remarks>
		protected abstract void LoadFromFile(StreamReader stream);
    }
}
