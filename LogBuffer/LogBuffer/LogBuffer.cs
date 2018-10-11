using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogBuffer
{
    class LogBuffer : IDisposable
    {
        /// <summary>
        /// Value determines whether Dispose() was called
        /// </summary>
        private bool DisposedValue = false;

        /// <summary>
        /// Object used for locking while writing
        /// </summary> 
        private object SynchronousWrite = new object();

        /// <summary>
        /// List of messages that should be added 
        /// to file on hard drive
        /// </summary>
        private List<string> Buffer = new List<string>();
        
        /// <summary>
        /// Appended message to buffer
        /// </summary>
        private string AppendedMessage
        {
            set
            {
                lock (Buffer)
                {
                    Buffer.Add(value);
                }
                
                if (Buffer.Count == BufferSize)
                {
                    lock (SynchronousWrite)
                    {
                        WriteMessagesAsync();
                    }                  
                }
            }
        }

        /// <summary>
        /// File that stores messages
        /// </summary>
        private string StorageFile;

        /// <summary>
        /// Time between writes on hard drive in ms
        /// </summary>
        private int ReleaseTime;

        /// <summary>
        /// Buffer capacity 
        /// </summary>
        private int BufferSize;

        private readonly int DefaultReleaseTimeInSeconds = 1_000;

        private readonly int DefaultBufferSize = 50;

        private List<Task> RunningTasks = new List<Task>();

        CancellationTokenSource Cts = new CancellationTokenSource();

        CancellationToken Ct;

        /// <summary>
        /// Creates imnstance of LogBuffer class
        /// </summary>
        /// <param name="BufferSize">Buffer capacity</param>
        /// <param name="ReleaseTime">Time between writes can't exceed this value</param>
        /// <param name="StorageFile">File that will store messages</param>
        public LogBuffer(int BufferSize, int ReleaseTime, string StorageFile)
        {
            this.BufferSize = (BufferSize > 0) ? BufferSize : DefaultBufferSize;
            this.ReleaseTime = (ReleaseTime > 0) ? ReleaseTime : DefaultReleaseTimeInSeconds;           
            this.StorageFile = StorageFile;
            Ct = Cts.Token;
            Task backgroundPeriodicWrite = new Task(BackgroundPeriodicWrite, Ct);
        }

        ~LogBuffer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Finalizes all running works
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    Task.WaitAll(RunningTasks.ToArray());
                    Cts.Cancel();
                    if (Buffer.Count != 0)
                    {
                        File.AppendAllLines(StorageFile, Buffer);
                    }
                }
                DisposedValue = true;
            }
        }

        /// <summary>
        /// Writes messages to file
        /// </summary>
        private void BackgroundPeriodicWrite()
        {
            while (true)
            {
                Thread.Sleep(ReleaseTime);           
                WriteMessagesAsync();              
            }
        }

        /// <summary>
        /// Appends string to buffer
        /// </summary>
        /// <param name="Item">Message ot add</param>
        public void Add(string Item)
        {
            AppendedMessage = Item;
        }

        /// <summary>
        /// Calls task to write buffer list to file
        /// </summary>
        private async void WriteMessagesAsync()
        {
            await AsyncWriteTask();
        }

        /// <summary>
        /// Writes buffer list to file 
        /// </summary>
        /// <returns></returns>
        private Task AsyncWriteTask()
        {
            List<string> bufferToWrite;
            lock (Buffer)
            {
                bufferToWrite = Buffer;
                Buffer = new List<string>();
            }                       
            var t = Task.Run(() =>
            {
                lock (StorageFile)
                {
                    File.AppendAllLines(StorageFile, bufferToWrite);
                }
            });
            RunningTasks.Add(t);
            return t;
        }            
    }
}
