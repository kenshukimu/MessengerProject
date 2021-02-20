using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.IPC
{
    /// <summary>
    /// 서버 시작상태
    /// </summary>
    public enum IpcStartStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        SUCCESS = 0,
        /// <summary>
        /// Failed to create pipe
        /// </summary>
        FAIL_PIPE_CREATE_FAILED,
    }

    /// <summary>
    /// 연결상태
    /// </summary>
    public enum IpcConnectStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        SUCCESS = 0,
        /// <summary>
        /// Failed to wait for connection
        /// </summary>
        FAIL_WAIT_FOR_CONNECTION_FAILED,
        /// <summary>
        /// Pipe open failed
        /// </summary>
        FAIL_PIPE_OPEN_FAILED,
        /// <summary>
        /// ReadMode Set failed
        /// </summary>
        FAIL_SET_READ_MODE_FAILED,
        /// <summary>
        /// Read failed
        /// </summary>
        FAIL_READ_FAILED,
        /// <summary>
        /// Timed Out
        /// </summary>
        FAIL_TIME_OUT,
    }


    /// <summary>
    /// 쓰기 상태
    /// </summary>
    public enum IpcWriteStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        SUCCESS = 0,
        /// <summary>
        /// Send failed
        /// </summary>
        FAIL_WRITE_FAILED,
    }

    /// <summary>
    /// Pipe write element
    /// </summary>
	public class PipeWriteElem
    {
        /// <summary>
        /// offset of start of data
        /// </summary>
        public int m_offset;
        /// <summary>
        /// /// Byte size of the data
        /// </summary>
        public int m_dataSize;
        /// <summary>
        /// Data buffer
        /// </summary>
        public byte[] m_data;


		public PipeWriteElem()
        {
            m_dataSize = 0;
            m_offset = 0;
            m_data = null;
        }


        public PipeWriteElem(byte[] data, int offset, int dataSize)
        {
            m_offset = offset;
            m_dataSize = dataSize;
            m_data = data;
        }

    }
    /// <summary>
    /// IPC configuration class
    /// </summary>
    public class IpcConf
    {
        /// <summary>
        /// Unlimited instance of pipe
        /// </summary>
        public const int DEFAULT_PIPE_INSTANCES = 255;
        /// <summary>
        /// Default write buffer size
        /// </summary>
        public const int DEFAULT_WRITE_BUF_SIZE = 4096;
        /// <summary>
        /// Default read buffer size
        /// </summary>
        public const int DEFAULT_READ_BUF_SIZE = 4096;
    }
}
