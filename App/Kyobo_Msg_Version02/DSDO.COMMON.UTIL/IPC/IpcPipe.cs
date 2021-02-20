﻿using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.IPC
{
    /// <summary>
    /// IPC Pipe Options
    /// </summary>
    public sealed class IpcPipeOps
    {
        /// <summary>
        /// Callback Object
        /// </summary>
        public IpcPipeCallbackInterface m_callBackObj;
        /// <summary>
        /// Name of the pipe
        /// </summary>
        public string m_pipeName;

        /// <summary>
        /// read byte size
        /// </summary>
        public int m_numOfReadBytes;
        /// <summary>
        /// write byte size
        /// </summary>
        public int m_numOfWriteBytes;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="pipeName">the name of the pipe</param>
        /// <param name="numOfReadyBytes">maximum read buffer size</param>
        /// <param name="numOfWriteBytes">maximum write buffer size</param>
        /// <param name="callBackObj">callback object</param>
        public IpcPipeOps(string pipeName, IpcPipeCallbackInterface callBackObj, int numOfReadyBytes = IpcConf.DEFAULT_READ_BUF_SIZE, int numOfWriteBytes = IpcConf.DEFAULT_WRITE_BUF_SIZE)
        {
            m_pipeName = pipeName;
            m_callBackObj = callBackObj;
            m_numOfReadBytes = numOfReadyBytes;
            m_numOfWriteBytes = numOfWriteBytes;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IpcPipeOps()
        {
            m_callBackObj = null;
            m_pipeName = null;
            m_numOfReadBytes = IpcConf.DEFAULT_READ_BUF_SIZE;
            m_numOfWriteBytes = IpcConf.DEFAULT_WRITE_BUF_SIZE;

        }

        /// <summary>
        /// Default IPC Pipe options
        /// </summary>
        public static IpcPipeOps defaultIpcPIpeOps;
    };

    /// <summary>
    /// Pipe Callback Interface
    /// </summary>
    public interface IpcPipeCallbackInterface
    {
        /// <summary>
        ///  When accepted client tries to make connection.
        /// </summary>
        /// <param name="pipe">the pipe</param>
        /// <param name="status">status of connect</param>
        /// <remarks>when this function calls, it is right before making connection,
        /// so user can configure the pipe before the connection is actually made.	</remarks>
        void OnNewConnection(IpcInterface pipe, IpcConnectStatus status);

        /// <summary>
        /// Received the data from the client.
        /// </summary>
        /// <param name="pipe">the pipe which received the packet</param>
        /// <param name="receivedData">the received data</param>
        /// <param name="receivedDataByteSize">the received data byte size</param>
        void OnReadComplete(IpcInterface pipe, byte[] receivedData, int receivedDataByteSize);

        /// <summary>
        /// Received the packet from the client.
        /// </summary>
        /// <param name="pipe">the pipe which wrote the packet</param>
        /// <param name="status">the status of write</param>
        void OnWriteComplete(IpcInterface pipe, IpcWriteStatus status);

        /// <summary>
        ///  The pipe is disconnected.
        /// </summary>
        /// <param name="pipe">the pipe, disconnected.</param>
        void OnDisconnected(IpcInterface pipe);
    };

    /// <summary>
    /// IPC Pipe class
    /// </summary>
    public sealed class IpcPipe : IpcInterface, IDisposable
    {

        /// <summary>
        /// Pipe handle
        /// </summary>
        private NamedPipeServerStream m_pipeHandle;
        /// <summary>
        /// flag whether the pipe is connected
        /// </summary>
        private bool m_connected;
        /// <summary>
        /// Pipe options
        /// </summary>
        private IpcPipeOps m_options;

        /// <summary>
        /// Pipe security
        /// </summary>
        private readonly PipeSecurity m_ps;

        /// <summary>
        /// Write buffer queue
        /// </summary>
        Queue<PipeWriteElem> m_writeQueue = new Queue<PipeWriteElem>();
        /// <summary>
        /// Read buffer
        /// </summary>
        byte[] m_readBuffer;

        /// <summary>
        /// General lock object
        /// </summary>
        Object m_generalLock = new Object();

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="options">the pipe options</param>
        public IpcPipe(IpcPipeOps options)
        {
            if (options == null)
                options = IpcPipeOps.defaultIpcPIpeOps;
            if (options.m_callBackObj != null)
                throw new ArgumentException("callBackObj is null!");
            lock (m_generalLock)
            {
                m_options = options;
                if (options.m_numOfWriteBytes == 0)
                    m_options.m_numOfWriteBytes = IpcConf.DEFAULT_WRITE_BUF_SIZE;
                if (options.m_numOfReadBytes == 0)
                    m_options.m_numOfReadBytes = IpcConf.DEFAULT_READ_BUF_SIZE;

            }
            m_readBuffer = new byte[m_options.m_numOfReadBytes];
            // Provide full access to the current user so more pipe instances can be created
            m_ps = new PipeSecurity();
            m_ps.AddAccessRule(
                new PipeAccessRule(WindowsIdentity.GetCurrent().User, PipeAccessRights.FullControl, AccessControlType.Allow)
            );
            m_ps.AddAccessRule(
                new PipeAccessRule(
                    new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null), PipeAccessRights.ReadWrite, AccessControlType.Allow
                )
            );
        }

        /// <summary>
        /// Default Destructor
        /// </summary>
		~IpcPipe()
        {
            Dispose(false);
        }

        /// <summary>
        /// Create the pipe
        /// </summary>
        /// <returns> true if successfully created otherwise false</returns>
		public bool Create()
        {
            try
            {
                m_pipeHandle = new NamedPipeServerStream(
                m_options.m_pipeName,
                PipeDirection.InOut,
               -1,     // maximum instances
                PipeTransmissionMode.Message,
                PipeOptions.Asynchronous | PipeOptions.WriteThrough,
                m_options.m_numOfWriteBytes,
                m_options.m_numOfReadBytes,
                m_ps
                );
                m_pipeHandle.BeginWaitForConnection(OnClientConnected, this);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                return false;
            }

        }

        /// <summary>
        /// Kill current connection and wait for other connection
        /// </summary>
        public void Reconnect()
        {
            KillConnection();
            Create();
        }

        /// <summary>
        /// Write data to the pipe
        /// </summary>
        /// <param name="data">the data to write</param>
        /// <param name="offset">offset to start write from given data</param>
        /// <param name="dataByteSize">byte size of the data to write</param>
		public void Write(byte[] data, int offset, int dataByteSize)
        {
            if (dataByteSize > m_options.m_numOfWriteBytes)
                throw new ArgumentException();
            PipeWriteElem elem = new PipeWriteElem(data, offset, dataByteSize);
            lock (m_writeQueue)
            {
                if (m_writeQueue.Count > 0)
                {
                    m_writeQueue.Enqueue(elem);
                }
                else
                {
                    try
                    {
                        m_pipeHandle.BeginWrite(elem.m_data, 0, elem.m_dataSize, OnWriteComplete, this);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                        if (IsConnectionAlive())
                            KillConnection();
                    }
                }
            }

        }

        /// <summary>
        /// Check if the connection is alive
        /// </summary>
        /// <returns>true if the connection is alive otherwise false</returns>
		public bool IsConnectionAlive()
        {
            return m_connected;
        }

        /// <summary>
        /// Kill the connection
        /// </summary>
		public void KillConnection()
        {
            lock (m_generalLock)
            {
                if (!IsConnectionAlive())
                {
                    return;
                }
                m_pipeHandle.Close();
                m_pipeHandle = null;
                m_connected = false;
            }

            lock (m_writeQueue)
            {
                m_writeQueue.Clear();
            }
            Task t = new Task(delegate ()
            {
                m_options.m_callBackObj.OnDisconnected(this);
            });
            t.Start();
        }

        /// <summary>
        /// Handle on client connected
        /// </summary>
        /// <param name="result">AsyncResult</param>
        private void OnClientConnected(IAsyncResult result)
        {
            IpcPipe pipeInst = (IpcPipe)result.AsyncState;
            // Complete the client connection
            NamedPipeServerStream pipe = (NamedPipeServerStream)result.AsyncState;
            try
            {
                pipe.EndWaitForConnection(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                pipeInst.m_options.m_callBackObj.OnNewConnection(this, IpcConnectStatus.FAIL_WAIT_FOR_CONNECTION_FAILED);
                return;
            }

            try
            {
                m_pipeHandle.BeginRead(m_readBuffer, 0, m_options.m_numOfReadBytes, OnReadComplete, this);
                m_connected = true;
                m_options.m_callBackObj.OnNewConnection(this, IpcConnectStatus.SUCCESS);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                KillConnection();
                m_options.m_callBackObj.OnNewConnection(this, IpcConnectStatus.FAIL_READ_FAILED);
            }

        }

        /// <summary>
        /// Handle when read is completed
        /// </summary>
        /// <param name="result">AsyncResult</param>
        private void OnReadComplete(IAsyncResult result)
        {
            IpcPipe pipeInst = (IpcPipe)result.AsyncState;
            int readByte = 0;
            byte[] readBuffer = null;
            try
            {
                readByte = pipeInst.m_pipeHandle.EndRead(result);
                readBuffer = m_readBuffer.ToArray();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                pipeInst.KillConnection();
                return;
            }
            try
            {
                pipeInst.m_pipeHandle.BeginRead(pipeInst.m_readBuffer, 0, pipeInst.m_options.m_numOfReadBytes, OnReadComplete, pipeInst);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                pipeInst.KillConnection();
            }

            pipeInst.m_options.m_callBackObj.OnReadComplete(pipeInst, readBuffer, readByte);

        }

        /// <summary>
        ///Handles when Write is completed
        /// </summary>
        /// <param name="result">AsyncResult</param>
		private void OnWriteComplete(IAsyncResult result)
        {
            IpcPipe pipeInst = (IpcPipe)result.AsyncState;

            try
            {
                pipeInst.m_pipeHandle.EndWrite(result);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                KillConnection();
                pipeInst.m_options.m_callBackObj.OnWriteComplete(pipeInst, IpcWriteStatus.FAIL_WRITE_FAILED);
                return;
            }

            lock (pipeInst.m_writeQueue)
            {
                if (pipeInst.m_writeQueue.Count > 0)
                {
                    PipeWriteElem elem = pipeInst.m_writeQueue.Dequeue();
                    if (pipeInst.m_writeQueue.Count() > 0)
                    {
                        PipeWriteElem nextElem = pipeInst.m_writeQueue.Dequeue();

                        try
                        {
                            m_pipeHandle.BeginWrite(nextElem.m_data, nextElem.m_offset, nextElem.m_dataSize, OnWriteComplete, this);
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                            pipeInst.m_options.m_callBackObj.OnWriteComplete(pipeInst, IpcWriteStatus.SUCCESS);
                            KillConnection();
                            pipeInst.m_options.m_callBackObj.OnWriteComplete(pipeInst, IpcWriteStatus.FAIL_WRITE_FAILED);
                            return;
                        }
                    }
                    pipeInst.m_options.m_callBackObj.OnWriteComplete(pipeInst, IpcWriteStatus.SUCCESS);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is disposed.
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
        private void Dispose(bool isDisposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    KillConnection();
                    if (isDisposing)
                    {
                        // Free any other managed objects here.
                        if (m_pipeHandle != null)
                        {
                            m_pipeHandle.Dispose();
                            m_pipeHandle = null;
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

    }
}
