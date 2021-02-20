﻿using DSDO.COMMON.UTIL.LOCK;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.IPC
{
    public sealed class IpcClient : ThreadEx, IpcClientInterface, IDisposable
    {
        /// <summary>
        /// connect wait time in milliseconds
        /// </summary>
        int m_connectWaitTimeInMillisec = 0;
        /// <summary>
        /// Pipe handle
        /// </summary>
        NamedPipeClientStream m_pipeHandle;
        /// <summary>
        /// flag whether the server is started
        /// </summary>
        bool m_connected;
        /// <summary>
        /// IPC client options
        /// </summary>
        IpcClientOps m_options;

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
        public IpcClient()
        {
            m_readBuffer = null;
        }

        /// <summary>
        /// Default Destructor
        /// </summary>
		~IpcClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Get the pipe name of server
        /// </summary>
        /// <returns> the pipe name in string</returns>
		public string GetFullPipeName()
        {
            return m_options.m_pipeName;
        }



        /// <summary>
        /// Connect to the server
        /// </summary>
        /// <param name="ops">the client options</param>
        /// <param name="waitTimeInMilliSec"> the wait time for connection in milli-second.</param>
        public void Connect(IpcClientOps ops, int waitTimeInMilliSec)
        {
            lock (m_generalLock)
            {
                if (IsConnected())
                    return;
            }

            if (ops == null)
                ops = IpcClientOps.defaultIpcClientOps;
            if (ops.m_callBackObj != null)
                throw new ArgumentException("callBackObj is null!");
            lock (m_generalLock)
            {
                m_options = ops;
                if (ops.m_numOfWriteBytes == 0)
                    m_options.m_numOfWriteBytes = IpcConf.DEFAULT_WRITE_BUF_SIZE;
                if (ops.m_numOfReadBytes == 0)
                    m_options.m_numOfReadBytes = IpcConf.DEFAULT_READ_BUF_SIZE;

            }
            m_readBuffer = new byte[m_options.m_numOfReadBytes];
            m_connectWaitTimeInMillisec = waitTimeInMilliSec;
            Start();

        }
        /// <summary>
        /// Actual connect function
        /// </summary>
        protected override void Execute()
        {
            lock (m_generalLock)
            {

                try
                {
                    m_pipeHandle = new NamedPipeClientStream(
                    m_options.m_domain,   // pipe name 
                    m_options.m_pipeName,
                    PipeDirection.InOut,
                    PipeOptions.Asynchronous | PipeOptions.WriteThrough
                    );          // no template file 

                    // Break if the pipe handle is valid. 
                    m_pipeHandle.Connect(m_connectWaitTimeInMillisec);

                }
                catch (TimeoutException)
                {
                    m_options.m_callBackObj.OnConnected(this, IpcConnectStatus.FAIL_TIME_OUT);
                    return;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                    m_options.m_callBackObj.OnConnected(this, IpcConnectStatus.FAIL_PIPE_OPEN_FAILED);
                    return;
                }


                try
                {
                    m_pipeHandle.ReadMode = PipeTransmissionMode.Message;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                    m_options.m_callBackObj.OnConnected(this, IpcConnectStatus.FAIL_SET_READ_MODE_FAILED);
                    return;
                }

            }
            startReceive();
        }

        /// <summary>
        /// start receiving from pipe
        /// </summary>
        private void startReceive()
        {

            try
            {
                m_pipeHandle.BeginRead(m_readBuffer, 0, m_options.m_numOfReadBytes, OnReadComplete, this);
                m_connected = true;
                m_options.m_callBackObj.OnConnected(this, IpcConnectStatus.SUCCESS);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                Disconnect();
                m_options.m_callBackObj.OnConnected(this, IpcConnectStatus.FAIL_READ_FAILED);
            }
        }

        /// <summary>
        /// Stop the server
        /// </summary>
		public void Disconnect()
        {
            lock (m_generalLock)
            {
                if (!IsConnected())
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
        /// Check if the client is connected to server
        /// </summary>
        /// <returns>true if the client is connected to server otherwise false</returns>

        public bool IsConnected()
        {
            return m_connected;
        }

        /// <summary>
        ///  Get the maximum write data byte size
        /// </summary>
        /// <returns>the maximum write data byte size</returns>
        public int GetMaxWriteDataByteSize()
        {
            return m_options.m_numOfWriteBytes;
        }

        /// <summary>
        /// Get the maximum read data byte size
        /// </summary>
        /// <returns>the maximum read data byte size</returns>
		public int GetMaxReadDataByteSize()
        {
            return m_options.m_numOfReadBytes;
        }


        /// <summary>
        /// Write data to the pipe
        /// </summary>
        /// <param name="data"> the data to write</param>
        /// <param name="offset">offset to start write from given data</param>
        /// <param name="dataByteSize">byte size of the data</param>
		public void Write(byte[] data, int offset, int dataByteSize)
        {
            if (dataByteSize > m_options.m_numOfWriteBytes)
                throw new ArgumentException();

            PipeWriteElem elem = new PipeWriteElem(data, dataByteSize, offset);

            lock (m_writeQueue)
            {
                if (m_writeQueue.Count() > 0)
                {
                    m_writeQueue.Enqueue(elem);
                }
                else
                {
                    try
                    {
                        m_pipeHandle.BeginWrite(elem.m_data, elem.m_offset, elem.m_dataSize, OnWriteComplete, this);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                        Disconnect();
                    }
                }
            }

        }

        /// <summary>
        /// Handles when Read is completed
        /// </summary>
        /// <param name="result">AsyncResult</param>
        private void OnReadComplete(IAsyncResult result)
        {
            IpcClient pipeInst = (IpcClient)result.AsyncState;
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
                Disconnect();
                return;
            }
            try
            {
                pipeInst.m_pipeHandle.BeginRead(pipeInst.m_readBuffer, 0, pipeInst.m_options.m_numOfReadBytes, OnReadComplete, pipeInst);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                Disconnect();
            }

            pipeInst.m_options.m_callBackObj.OnReadComplete(pipeInst, readBuffer, readByte);
        }

        /// <summary>
        /// Handles when Write is completed
        /// </summary>
        /// <param name="result">AsyncResult</param>
        private void OnWriteComplete(IAsyncResult result)
        {
            IpcClient pipeInst = (IpcClient)result.AsyncState;

            try
            {
                pipeInst.m_pipeHandle.EndWrite(result);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " >" + ex.StackTrace);
                Disconnect();
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
                            Disconnect();
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
                    Disconnect();
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
