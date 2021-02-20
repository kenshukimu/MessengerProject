using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.FILESYSTEM
{
    public sealed class TextFile : BaseTextFile
    {
        /// <summary>
        /// txt 값
        /// </summary>
        private String m_text;


		public TextFile(Encoding encoding = null) : base(encoding)
        {
            m_text = "";
        }


		public TextFile(TextFile b) : base(b)
        {
            lock (m_baseTextLock)
            {
                m_text = b.m_text;
            }
        }


        /// <summary>
        /// txt 값 설정
        /// </summary>
        /// <param name="val">the text value</param>
		public void SetText(String val)
        {
            lock (m_baseTextLock)
            {
                m_text = val;
            }
        }


        /// <summary>
        /// txt 값 반환
        /// </summary>
        /// <returns>text value holding</returns>
		public String GetText()
        {
            lock (m_baseTextLock)
            {
                return m_text;
            }
        }


        /// <summary>
        /// txt 값 삭제
        /// </summary>
        public void Clear()
        {
            lock (m_baseTextLock)
            {
                m_text = "";
            }
        }


        /// <summary>
        /// 파일에 쓰는  Loop 함수
        /// </summary>
        protected override void WriteLoop()
        {
            WriteToFile(m_text);
        }

        /// <summary>
        /// 실제로드 파일에서 값을로드하는 기능
        /// </summary>
        /// <param name="stream">the stream from the file</param>
        protected override void LoadFromFile(StreamReader stream)
        {
            m_text = stream.ReadToEnd();
        }

    }
}
