using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.LOCK
{
    public class MutexEx : BaseLock, IDisposable
    {
        /// <summary>
        /// lock
        /// </summary>
        private Mutex m_mutex;

        /// <summary>
        /// 뮤텍스가 생성시 소유되었는지 여부 플래그
        /// </summary>
        private bool m_isInitialOwned;

        /// <summary>
        /// 뮤텍스의 이름
        /// </summary>
        private String m_name;

        /// <summary>
        /// 뮤텍스가 버려지는지 여부 플래그
        /// </summary>
        private bool m_isMutexAbandoned;


        public MutexEx(String mutexName = null)
            : base()
        {
            m_isInitialOwned = false;
            m_name = mutexName;
            m_isMutexAbandoned = false;
            m_mutex = new Mutex(m_isInitialOwned, m_name);
        }


        public MutexEx(bool isInitialOwned, String mutexName = null)
        {
            m_isInitialOwned = isInitialOwned;
            m_name = mutexName;
            m_isMutexAbandoned = false;
            m_mutex = new Mutex(m_isInitialOwned, m_name);
        }

        
		public MutexEx(MutexEx b) : base(b)
        {
            m_isInitialOwned = b.m_isInitialOwned;
            m_name = b.m_name;
            m_isMutexAbandoned = false;
            m_mutex = new Mutex(m_isInitialOwned, m_name);
        }


        /// <summary>
        /// 임계 영역을 잠금
        /// </summary>
        /// <returns>true if locked, otherwise false</returns>
        public override bool Lock()
        {
            try
            {
                return m_mutex.WaitOne();
            }
            catch (AbandonedMutexException ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                m_isMutexAbandoned = true;
                return false;
            }
        }

        /// <summary>
        /// 다른 thread가 이미 크리티컬 섹션에 있으면, false를 반환하며 진행하고, 그렇지 않으면 ciritical 섹션을 얻는다.
        /// </summary>
        /// <returns>true if locked, otherwise false</returns>
        public override bool TryLock()
        {
            try
            {
                return m_mutex.WaitOne(0);
            }
            catch (AbandonedMutexException ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                m_isMutexAbandoned = true;
                return false;
            }

        }

        /// <summary>
        /// 주어진 시간 동안 중요한 부분을 잠금
        /// </summary>
        /// <param name="dwMilliSecond">the wait time</param>
        /// <returns>true if locked, otherwise false</returns>
        public override bool TryLockFor(int dwMilliSecond)
        {
            try
            {
                return m_mutex.WaitOne(dwMilliSecond);
            }
            catch (AbandonedMutexException ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                m_isMutexAbandoned = true;
                return false;
            }

        }

        /// <summary>
        /// 임계영역을나감
        /// </summary>
		public override void Unlock()
        {
            m_mutex.ReleaseMutex();
        }

        /// <summary>
        /// 뮤텍스가 버려 졌는지 여부를 나타내는 플래그를 반환
        /// </summary>
        /// <returns>true if the mutex is abandoned, otherwise false</returns>
        public bool IsMutexAbandoned()
        {
            return m_isMutexAbandoned;
        }

        
        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (m_mutex != null)
                        {
                            m_mutex.Dispose();
                            m_mutex = null;
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

        ~MutexEx() { Dispose(false); }
    }
}
