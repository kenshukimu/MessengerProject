using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.LOCK
{
    public sealed class NoLock : BaseLock
    {
        public NoLock() : base()
        {
        }
        
        public NoLock(NoLock b)
            : base(b)
        {
        }


        /// <summary>
        /// 임계 영역을 잠금
        /// </summary>
        /// <returns>true if locked, otherwise false</returns>
        public override bool Lock()
        {
            return true;
        }

        /// <summary>
        /// 다른 thread가 이미 크리티컬 섹션에 있으면, false를 반환하며 진행하고, 그렇지 않으면 ciritical 섹션을 얻는다.
        /// </summary>
        /// <returns>true if locked, otherwise false</returns>
        public override bool TryLock()
        {
            return true;
        }

        /// <summary>
        /// 주어진 시간 동안 중요한 부분을 잠금
        /// </summary>
        /// <param name="dwMilliSecond">the wait time</param>
        /// <returns>true if locked, otherwise false</returns>
        public override bool TryLockFor(int dwMilliSecond)
        {
            return true;
        }

        /// <summary>
        /// 임계영역을나감
        /// </summary>
        public override void Unlock()
        {
        }
    }
}
