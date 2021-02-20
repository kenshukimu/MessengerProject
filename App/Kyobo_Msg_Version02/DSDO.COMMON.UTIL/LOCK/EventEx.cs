using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.LOCK
{
    public sealed class EventEx : BaseLock, IDisposable
    {
        /// <summary>
        /// event
        /// </summary>
        private EventWaitHandle m_event;
        /// <summary>
        /// 생성시 이벤트가 발생하는지 여부를 나타내는 플래그
        /// </summary>
		private bool m_isInitialRaised;
        /// <summary>
        /// EventResetMode
        /// </summary>
		private EventResetMode m_eventResetMode;
        /// <summary>
        /// event name
        /// </summary>
        private String m_name;


        public EventEx(String eventName = null)
            : base()
        {
            m_eventResetMode = EventResetMode.AutoReset;
            m_isInitialRaised = true;
            m_name = eventName;
            if (m_name == null)
                m_event = new EventWaitHandle(m_isInitialRaised, m_eventResetMode);
            else
                m_event = new EventWaitHandle(m_isInitialRaised, m_eventResetMode, m_name);
        }

        
        public EventEx(bool isInitialRaised, EventResetMode eventResetMode, String eventName = null) : base()
        {
            m_eventResetMode = eventResetMode;
            m_isInitialRaised = isInitialRaised;
            m_name = eventName;
            if (m_name == null)
                m_event = new EventWaitHandle(m_isInitialRaised, m_eventResetMode);
            else
                m_event = new EventWaitHandle(m_isInitialRaised, m_eventResetMode, m_name);
        }

        
		public EventEx(EventEx b) : base(b)
        {
            m_isInitialRaised = b.m_isInitialRaised;
            m_name = b.m_name;
            m_eventResetMode = b.m_eventResetMode;
            if (m_name == null)
                m_event = new EventWaitHandle(m_isInitialRaised, m_eventResetMode);
            else
                m_event = new EventWaitHandle(m_isInitialRaised, m_eventResetMode, m_name);
        }


        /// <summary>
        /// 크리티컬 섹션을 잠금
        /// </summary>
        /// <returns>true if locked, otherwise false</returns>
		public override bool Lock()
        {
            return m_event.WaitOne();
        }

        /// <summary>
        /// 다른 thread 가 이미 크리티컬 섹션에 있으면 false를 반환, 그렇지 않으면 ciritical 섹션을 얻는다.
        /// </summary>
        /// <returns>true if locked, otherwise false</returns>
		public override bool TryLock()
        {
            return m_event.WaitOne(0);
        }

        /// <summary>
        /// 주어진 시간 동안 크리티컬 섹션을 잠금
        /// </summary>
        /// <param name="dwMilliSecond">the wait time</param>
        /// <returns>true if locked, otherwise false</returns>
		public override bool TryLockFor(int dwMilliSecond)
        {
            return m_event.WaitOne(dwMilliSecond);
        }

        /// <summary>
        /// 크리티컬 섹션나감
        /// </summary>
		public override void Unlock()
        {
            m_event.Set();
        }

        /// <summary>
        /// 이벤트 재설정
        /// </summary>
        /// <returns>true if succeeded otherwise false</returns>
        /// <remarks>if event is not raised then no effect</remarks>
		public bool ResetEvent()
        {
            return m_event.Reset();
        }

        /// <summary>
        /// 이벤트 설정
        /// </summary>
        /// <returns>true if succeeded otherwise false</returns>
        /// <remarks>
        /// if event is already raised then no effect
        /// this function is same as unlock
        /// </remarks>
        public bool SetEvent()
        {
            return m_event.Set();
        }

        /// <summary>
        /// 이 이벤트가 수동으로 리셋되고 있을지를 나타내는 플래그를 반환
        /// </summary>
        /// <returns>EventResetMode</returns>
        public EventResetMode GetEventResetMode()
        {
            return m_eventResetMode;
        }

        /// <summary>
        /// 주어진 시간 동안 이벤트가 발생할 때까지 대기
        /// </summary>
        /// <param name="dwMilliSecond">the wait time in millisecond</param>
        /// <returns>true if the wait is succeeded, otherwise false</returns>
        public bool WaitForEvent(int dwMilliSecond = Timeout.Infinite)
        {
            return m_event.WaitOne(dwMilliSecond);
        }

        /// <summary>
        /// 실제 이벤트 가져 오기
        /// </summary>
        /// <returns>actual event</returns>
        public EventWaitHandle GetEventHandle()
        {
            return m_event;
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
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (m_event != null)
                        {
                            m_event.Dispose();
                            m_event = null;
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

        ~EventEx() { Dispose(false); }

    }
}
