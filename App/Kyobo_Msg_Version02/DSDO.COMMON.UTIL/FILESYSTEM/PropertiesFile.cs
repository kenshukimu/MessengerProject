using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.FILESYSTEM
{
    public sealed class PropertiesFile : BaseTextFile
    {
        /// <summary>
        /// 속성 목록
        /// </summary>
        private Dictionary<String, String> m_propertyList = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
		public PropertiesFile(Encoding encoding = null) : base(encoding)
        {
        }
        
        public PropertiesFile(PropertiesFile b)
            : base(b)
        {
            lock (m_baseTextLock)
            {
                m_propertyList = new Dictionary<string, string>(b.m_propertyList, StringComparer.OrdinalIgnoreCase);
            }
        }



        /// <summary>
        /// 지정된 키를 지정해 주어진 값으로 설정
        /// </summary>
        /// <param name="key">the key of the property to change the value</param>
        /// <param name="val">the value to change the property</param>
        /// <returns></returns>
        public void SetProperty(String key, String val)
        {
            lock (m_baseTextLock)
            {
                String opKey = key.Trim();
                opKey += "=";
                m_propertyList[opKey] = val.Trim();
            }

        }


        /// <summary>
        /// 지정된 키를 가지는 값 여부 반환
        /// </summary>
        /// <param name="key">the key of the property to get the value</param>
        /// <param name="retVal">the value of the property of given key</param>
        /// <returns>true if found, otherwise false</returns>
        public bool GetProperty(String key, ref String retVal)
        {
            lock (m_baseTextLock)
            {
                String opKey = key.Trim();
                opKey += "=";
                if (m_propertyList.ContainsKey(opKey))
                {
                    retVal = m_propertyList[opKey];
                    return true;
                }
                return false;
            }
        }


        /// <summary>
        /// 지정된 키를 가지는 값 반환
        /// </summary>
        /// <param name="key">the key of the property to get the value</param>
        /// <returns>the value of the property of given key</returns>
        /// <remarks>raises exception when key does not exists</remarks>
        public String GetProperty(String key)
        {
            lock (m_baseTextLock)
            {
                String opKey = key.Trim();
                opKey += "=";
                return m_propertyList[opKey];
            }
        }

        /// <summary>
        /// 키와 값으로 새 속성 추가가능여부
        /// </summary>
        /// <param name="key">the key of the property to add</param>
        /// <param name="val">the value of the new property</param>
        /// <returns>true if successfully added, otherwise false</returns>
        public bool AddProperty(String key, String val)
        {
            lock (m_baseTextLock)
            {
                String opKey = key.Trim();
                opKey += "=";
                if (m_propertyList.ContainsKey(opKey))
                    return false;
                m_propertyList.Add(opKey, val.Trim());
                return true;
            }
        }


        /// <summary>
        /// 키로 값 제거
        /// </summary>
        /// <param name="key">the key of the property to remove</param>
        /// <returns>true if successfully removed, otherwise false</returns>
        public bool RemoveProperty(String key)
        {
            lock (m_baseTextLock)
            {
                String opKey = key.Trim();
                opKey += "=";
                return m_propertyList.Remove(opKey);
            }
        }

        /// <summary>
        /// 속성값 지우기
        /// </summary>
        public void Clear()
        {
            lock (m_baseTextLock)
            {
                m_propertyList.Clear();
            }
        }

        /// <summary>
        /// 지정된 키가 존재하는 경우는 그 값을 돌려주고,  지정된 키가 존재하지 않는 경우는 키를 작성하며 값을 추가
        /// </summary>
        /// <param name="key">the key of the property to find/create</param>
        /// <returns>value of the given key.</returns>
		public String this[String key]
        {
            get
            {
                lock (m_baseTextLock)
                {
                    String opKey = key.Trim();
                    opKey += "=";
                    if (m_propertyList.ContainsKey(opKey))
                        return m_propertyList[opKey];
                    m_propertyList.Add(opKey, "");
                    return m_propertyList[opKey];
                }
            }
            set
            {
                lock (m_baseTextLock)
                {
                    String opKey = key.Trim();
                    opKey += "=";
                    m_propertyList[opKey] = value;
                }
            }
        }


        /// <summary>
        /// 파일에 쓰는  Loop 함수
        /// </summary>
		protected override void WriteLoop()
        {
            StringBuilder toFileString = new StringBuilder();
            foreach (KeyValuePair<String, String> entry in m_propertyList)
            {
                toFileString.Clear();
                toFileString.Append(entry.Key);
                toFileString.Append(entry.Value);
                toFileString.Append("\n");
                WriteToFile(toFileString.ToString());
            }
        }

        /// <summary>
        /// 실제로드 파일에서 값을로드하는 기능
        /// </summary>
        /// <param name="stream">the stream from the file</param>
		protected override void LoadFromFile(StreamReader stream)
        {
            m_propertyList.Clear();
            String line = "";
            line = stream.ReadLine();
            while (line != null)
            {
                String key = "";
                String val = "";
                if (GetValueKeyFromLine(line, ref key, ref val))
                {
                    key = key.Trim();
                    val = val.Trim();
                    m_propertyList.Add(key, val);
                }
                else
                {
                    m_propertyList.Add(line, "");
                }
                line = stream.ReadLine();

            }
        }

        /// <summary>
        /// 버퍼에서 키와 값을 파싱
        /// </summary>
        /// <param name="buf">the buffer that holds a line</param>
        /// <param name="retKey">the key part of the given line</param>
        /// <param name="retVal">the value part of the given line</param>
        /// <returns>true if successfully parsed the key and value, otherwise false</returns>
        private bool GetValueKeyFromLine(String buf, ref String retKey, ref String retVal)
        {
            char splitChar = '\0';
            int bufTrav = 0;
            if (buf.Length <= 0)
                return false;

            retKey = "";
            retVal = "";
            StringBuilder builder = new StringBuilder();
            buf = buf.Trim();

            if (buf[0] == '#')
                return false;

            while (splitChar != '=' && bufTrav < buf.Length)
            {
                splitChar = buf[bufTrav];
                builder.Append(splitChar);
                bufTrav++;
            }
            retKey = builder.ToString();
            retVal = buf;
            retVal = retVal.Remove(0, bufTrav);

            return true;
        }

    }
}
