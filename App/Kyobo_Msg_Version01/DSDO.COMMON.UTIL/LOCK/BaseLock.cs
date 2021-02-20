using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.LOCK
{
    public abstract class BaseLock
    {
        public BaseLock()
        {
        }
        public BaseLock(BaseLock b)
        {
        }


        /// <summary>
        /// 임계 영역을 잠금
        /// </summary>
        /// <returns>true if locked, otherwise false</returns>
		public abstract bool Lock();

        /// <summary>
        /// 다른 thread가 이미 크리티컬 섹션에 있으면, false를 반환하며 진행하고, 그렇지 않으면 ciritical 섹션을 얻는다.
        /// </summary>
        /// <returns>true if locked, otherwise false</returns>
        public abstract bool TryLock();

        /// <summary>
        /// 주어진 시간 동안 중요한 부분을 잠금
        /// </summary>
        /// <param name="dwMilliSecond">the wait time</param>
        /// <returns>true if locked, otherwise false</returns>
        public abstract bool TryLockFor(int dwMilliSecond);


        /// <summary>
        /// 임계영역을나감
        /// </summary>
        public abstract void Unlock();

        /// <summary>
        /// 잠금을 처리하는 클래스.
        /// </summary>
		public class BaseLockObj
        {

            /// <summary>
            /// lock
            /// </summary>
            BaseLock m_lock;


            /// <summary>
            /// Default Constructor
            /// </summary>
            /// <param name="iLock">the lock to lock.</param>
			public BaseLockObj(BaseLock iLock)
            {
                Debug.Assert(iLock != null, "Lock is null!");
                m_lock = iLock;
                if (m_lock != null)
                    m_lock.Lock();
            }

            
			~BaseLockObj()
            {
                if (m_lock != null)
                {
                    m_lock.Unlock();
                }
            }
            
			private BaseLockObj()
            {
                m_lock = null;
            }

        };

    }
}
