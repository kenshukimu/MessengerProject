using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.LOCK
{
    public enum ThreadOpCode
    {
        /// <summary>
        /// The thread is started when it is created.
        /// </summary>
        CREATE_START = 0,
        /// <summary>
        /// The thread is suspended when it is created.
        /// </summary>
        CREATE_SUSPEND
    };

    /// <summary>
    /// 스레드 상태에 대한 열거
    /// </summary>
    public enum ThreadStatus
    {
        /// <summary>
        /// The thread is started and running.
        /// </summary>
        STARTED = 0,
        /// <summary>
        /// The thread is suspended.
        /// </summary>
        SUSPENDED,
        /// <summary>
        /// The thread is terminated.
        /// </summary>
        TERMINATED
    };

    /// <summary>
    /// 스레드 종료 결과 열거
    /// </summary>
    public enum TerminateResult
    {
        /// <summary>
        /// Failed to terminate the thread 
        /// </summary>
        FAILED = 0,
        /// <summary>
        /// The thread terminated gracefully
        /// </summary>
        GRACEFULLY_TERMINATED,
        /// <summary>
        /// The thread terminated forcefully
        /// </summary>
        FORCEFULLY_TERMINATE,
        /// <summary>
        /// The thread was not running
        /// </summary>
        NOT_ON_RUNNING,
    };


    /// <summary>
    /// 기본 스레드 클래스 작업을 구현하는 클래스
    /// </summary>
    public class ThreadEx
    {
        /// <summary>
        /// thread handle
        /// </summary>
        private Thread m_threadHandle;

        /// <summary>
        /// ThreadPriority
        /// </summary>
        private ThreadPriority m_threadPriority;

        /// <summary>
        /// Parent Thread Handle
        /// </summary>
        private Thread m_parentThreadHandle;

        /// <summary>
        /// Thread Status
        /// </summary>
        private ThreadStatus m_status;

        /// <summary>
        /// thread Func
        /// </summary>
        private Action m_threadFunc;

        /// <summary>
        /// thread parameterized Func
        /// </summary>
        private Action<object> m_threadParameterizedFunc;
        /// <summary>
        /// parameter object for parameterized function
        /// </summary>
        private object m_parameter;
        /// <summary>
        /// Lock
        /// </summary>
        private Object m_threadLock = new Object();
        /// <summary>
        /// exit code
        /// </summary>
		private ulong m_exitCode;

                
        public ThreadEx(ThreadPriority priority = ThreadPriority.Normal)
        {
            m_threadHandle = null;
            m_threadPriority = priority;
            m_parentThreadHandle = null;
            m_status = ThreadStatus.TERMINATED;
            m_exitCode = 0;
            m_threadFunc = null;
            m_threadParameterizedFunc = null;
            m_parameter = null;
        }
        
		public ThreadEx(Action threadFunc, ThreadPriority priority = ThreadPriority.Normal)
        {
            m_threadHandle = null;
            m_threadPriority = priority;
            m_parentThreadHandle = null;
            m_status = ThreadStatus.TERMINATED;
            m_exitCode = 0;
            m_threadFunc = threadFunc;
            m_threadParameterizedFunc = null;
            m_parameter = null;

            m_parentThreadHandle = Thread.CurrentThread;
            m_threadHandle = new Thread(ThreadEx.EntryPoint);
            m_threadHandle.Priority = m_threadPriority;
            m_threadHandle.Start(this);
            m_status = ThreadStatus.STARTED;


        }


        public ThreadEx(Action<object> threadParameterizedFunc, object parameter, ThreadPriority priority = ThreadPriority.Normal)
        {
            m_threadHandle = null;
            m_threadPriority = priority;
            m_parentThreadHandle = null;
            m_status = ThreadStatus.TERMINATED;
            m_exitCode = 0;
            m_threadFunc = null;
            m_threadParameterizedFunc = threadParameterizedFunc;
            m_parameter = parameter;

            m_parentThreadHandle = Thread.CurrentThread;
            m_threadHandle = new Thread(ThreadEx.EntryPoint);
            m_threadHandle.Priority = m_threadPriority;
            m_threadHandle.Start(this);
            m_status = ThreadStatus.STARTED;


        }


        public ThreadEx(ThreadEx b)
        {
            m_threadFunc = b.m_threadFunc;
            m_threadParameterizedFunc = b.m_threadParameterizedFunc;
            m_parameter = b.m_parameter;
            if (m_threadFunc != null || m_parentThreadHandle != null)
            {
                m_parentThreadHandle = b.m_parentThreadHandle;
                m_threadHandle = b.m_threadHandle;
                m_threadPriority = b.m_threadPriority;
                m_status = b.m_status;
                m_exitCode = b.m_exitCode;

                b.m_parentThreadHandle = null;
                b.m_threadHandle = null;
                b.m_status = ThreadStatus.TERMINATED;
                b.m_exitCode = 0;
            }
            else
            {
                m_threadHandle = null;
                m_threadPriority = b.m_threadPriority;
                m_parentThreadHandle = null;
                m_exitCode = 0;

                m_status = ThreadStatus.TERMINATED;
            }
        }

        ~ThreadEx()
        {
            ResetThread();
        }

        /// <summary>
        /// 주어진 매개 변수에 따라 스레드를 시작
        /// </summary>
        /// <param name="opCode">The operation code for creating thread.</param>
        /// <param name="stackSize">The stack size for the thread.</param>
        /// <returns>true, if succeeded, otherwise false.</returns>
        public bool Start(ThreadOpCode opCode = ThreadOpCode.CREATE_START, int stackSize = 0)
        {
            lock (m_threadLock)
            {
                m_parentThreadHandle = Thread.CurrentThread;
                if (m_status == ThreadStatus.TERMINATED && m_threadHandle == null)
                {
                    m_threadHandle = new Thread(ThreadEx.EntryPoint, stackSize);
                    if (m_threadHandle != null)
                    {
                        m_threadHandle.Priority = m_threadPriority;
                        if (opCode == ThreadOpCode.CREATE_START)
                        {
                            m_threadHandle.Start(this);
                            m_status = ThreadStatus.STARTED;
                        }
                        else
                            m_status = ThreadStatus.SUSPENDED;
                        return true;
                    }

                }
                //	System::OutputDebugString(_T("The thread (%x): Thread already exists!\r\n"),m_threadId);
                return false;
            }
        }

        /// <summary>
        /// 일시 중단 된 스레드를 재개
        /// </summary>
        /// <returns>true, if succeeded, otherwise false.</returns>
        public bool Resume()
        {
            lock (m_threadLock)
            {
                if (m_status == ThreadStatus.SUSPENDED && m_threadHandle != null)
                {
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
                    m_threadHandle.Resume();
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.

                    m_status = ThreadStatus.STARTED;
                    return true;
                }
            }
            //	System::OutputDebugString(_T("The thread (%x): Thread must be in suspended state in order to resume!\r\n"),m_threadId);
            return false;
        }

        /// <summary>
        /// 실행중인 스레드를 일시 중단
        /// </summary>
        /// <returns>true, if succeeded, otherwise false.</returns>
        public bool Suspend()
        {

            if (m_status == ThreadStatus.STARTED && m_threadHandle != null)
            {
                lock (m_threadLock)
                {
                    m_status = ThreadStatus.SUSPENDED;
                }
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
                m_threadHandle.Suspend();
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.
                return true;
            }
            //	System::OutputDebugString(_T("The thread (%x): Thread must be in running state in order to suspend!\r\n"),m_threadId);
            return false;

        }

        /// <summary>
        /// 실행 중 또는 중단 한 thread를 종료
        /// </summary>
        /// <returns>true, if succeeded, otherwise false.</returns>
        public bool Terminate()
        {
            Debug.Assert(m_threadHandle != Thread.CurrentThread, "Exception : Thread should not terminate self.");

            if (m_status != ThreadStatus.TERMINATED && m_threadHandle != null)
            {
                lock (m_threadLock)
                {
                    m_status = ThreadStatus.TERMINATED;
                    m_exitCode = 1;
                    m_threadHandle.Abort();
                    m_threadHandle = null;
                    m_parentThreadHandle = null;
                }
                ulong exitCode = m_exitCode;
                OnTerminated(exitCode);
                return true;
            }
            return true;
        }

        /// <summary>
        /// 스레드가 종료 될 때까지 대기
        /// </summary>
        /// <param name="tMilliseconds">the time-out interval, in milliseconds.</param>
        /// <returns>true if successful, otherwise false</returns>
        public bool WaitFor(int tMilliseconds = Timeout.Infinite)
        {
            if (m_status != ThreadStatus.TERMINATED && m_threadHandle != null)
            {
                return m_threadHandle.Join(tMilliseconds);
            }
            else
            {
                //	System::OutputDebugString(_T("The thread (%x): Thread is not started!\r\n"),m_threadId);
                return false;
            }
        }

        /// <summary>
        /// 스레드에 가입
        /// </summary>
        public void Join()
        {
            if (m_status != ThreadStatus.TERMINATED && m_threadHandle != null)
            {
                m_threadHandle.Join();
            }
        }

        /// <summary>
        /// 스레드 클래스가 조인 가능한지 확인
        /// </summary>
        /// <returns>true if joinable otherwise false</returns>
        public bool Joinable()
        {
            return (m_status != ThreadStatus.TERMINATED && m_threadHandle != null);
        }

        /// <summary>
        /// 스레드 분리
        /// </summary>
        public void Detach()
        {
            Debug.Assert(Joinable() == true);
            lock (m_threadLock)
            {
                m_status = ThreadStatus.TERMINATED;
                m_threadHandle = null;
                m_parentThreadHandle = null;
                m_exitCode = 0;
            }
        }

        /// <summary>
        /// 스레드가 종료 될 때까지 기다렸다가 종료되지 않은 경우 종료
        /// </summary>
        /// <param name="tMilliseconds">the time-out interval, in milliseconds.</param>
        /// <returns>the terminate result of the thread</returns>
        public TerminateResult TerminateAfter(int tMilliseconds)
        {
            if (m_status != ThreadStatus.TERMINATED && m_threadHandle != null)
            {
                bool status = m_threadHandle.Join(tMilliseconds);
                if (status)
                {
                    return TerminateResult.GRACEFULLY_TERMINATED;
                }
                else
                {
                    if (Terminate())
                        return TerminateResult.FORCEFULLY_TERMINATE;
                    return TerminateResult.FAILED;
                }
            }
            else
            {
                //System::OutputDebugString(_T("The thread (%x): Thread is not started!\r\n"),m_threadId);
                return TerminateResult.NOT_ON_RUNNING;
            }
        }

        /// <summary>
        /// 부모 thread 핸들을반환 
        /// </summary>
        /// <returns>the parent's Thread Handle.</returns>
		public Thread GetParentThreadHandle()
        {
            return m_parentThreadHandle;
        }

        /// <summary>
        /// thread 상태를반환
        /// </summary>
        /// <returns>the current thread status</returns>
		public ThreadStatus GetStatus()
        {
            return m_status;
        }

        /// <summary>
        /// 스레드 종료 코드를반환
        /// </summary>
        /// <returns>the thread exit code.</returns>
        /// <remarks>0 means successful termination, 1 means unsafe termination.</remarks>
		public ulong GetExitCode()
        {
            return m_exitCode;
        }

        /// <summary>
        /// 현재의 thread 우선 순위를반환
        /// </summary>
        /// <returns>the current Thread Priority.</returns>
        public ThreadPriority GetPriority()
        {
            return m_threadPriority;
        }

        /// <summary>
        /// 스레드의 우선 순위 설정
        /// </summary>
        /// <param name="priority">The priority of the thread</param>
        /// <returns>true if successfully set otherwise false</returns>
        public bool SetPriority(ThreadPriority priority)
        {
            m_threadPriority = priority;
            m_threadHandle.Priority = priority;
            return true;
        }

        /// <summary>
        /// 스레드 핸들을 반환
        /// </summary>
        /// <returns>the current thread handle.</returns>
		protected Thread GetHandle()
        {
            return m_threadHandle;
        }


        /// <summary>
        /// 실제 쓰레드 코드
        /// </summary>
        /// <remarks>Subclass should override this function for executing the thread function.</remarks>
        protected virtual void Execute()
        {
            if (m_threadFunc != null)
                m_threadFunc();
            else if (m_threadParameterizedFunc != null)
                m_threadParameterizedFunc(m_parameter);
        }

        /// <summary>
        /// 스레드가 종료 될 때 호출
        /// </summary>
        /// <param name="exitCode">the exit code of the thread</param>
        /// <param name="isInDeletion">the flag whether the thread class is in deletion or not</param>
        protected virtual void OnTerminated(ulong exitCode, bool isInDeletion = false)
        {
        }

        /// <summary>
        /// 스레드를 성공적으로 종료
        /// </summary>
        private void SuccessTerminate()
        {
            lock (m_threadLock)
            {
                m_status = ThreadStatus.TERMINATED;
                m_threadHandle = null;
                m_parentThreadHandle = null;
                m_exitCode = 0;
            }

            OnTerminated(m_exitCode);
        }

        /// <summary>
        /// 스레드가 생성 될 때 스레드를 실행
        /// </summary>
        /// <returns>the exit code of the current thread.</returns>
        private int Run()
        {
            Execute();
            SuccessTerminate();
            return 0;
        }
        /// <summary>
        /// 스레드 재설정
        /// </summary>
        private void ResetThread()
        {
            if (m_status != ThreadStatus.TERMINATED)
            {
                m_exitCode = 1;
                m_threadHandle.Abort();
                OnTerminated(m_exitCode, true);
            }

            m_threadHandle = null;
            m_parentThreadHandle = null;
            m_exitCode = 0;
            m_status = ThreadStatus.TERMINATED;
        }

        /// <summary>
        /// 스레드의 진입 점
        /// </summary>
        /// <param name="pThis">The argument for the thread (this for current case)</param>
        private static void EntryPoint(object pThis)
        {
            ThreadEx pt = (ThreadEx)pThis;
            pt.Run();
        }


    }
}
