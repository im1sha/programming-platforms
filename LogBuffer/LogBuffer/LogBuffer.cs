using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogBuffer
{
    class LogBuffer
    {
        /// <summary>
        /// List of messages that should be added 
        /// to file on hard drive
        /// </summary>
        private List<string> Buffer = new List<string>();

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

        CancellationTokenSource TokenSource = new CancellationTokenSource();

        CancellationToken Token;

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
            Token = TokenSource.Token;
            Task backgroundWrite = new Task(BackgroundWrite, Token);
        }

        /// <summary>
        /// Writes all 
        /// </summary>
        private void BackgroundWrite()
        {
            while (true)
            {
                Thread.Sleep(ReleaseTime);
                lock (Buffer)
                {
                    AsyncWrite(Buffer, StorageFile);
                }
            }
        }

        /// <summary>
        /// Appends string to buffer
        /// </summary>
        /// <param name="Item">Message ot add</param>
        public void Add(string Item)
        {
            if (Buffer.Count < BufferSize)
            {
                lock(Buffer)
                {
                    Buffer.Add(Item);
                }                
            }
            else if (Buffer.Count == BufferSize)
            {
                AsyncWrite(Buffer, StorageFile);
            }
        }

        /// <summary>
        /// Writes messages from list to file asynchronously
        /// </summary>
        /// <param name="MessageList">List with messages</param>
        /// <param name="StorageFile">File that will store messages</param>
        private void AsyncWrite(List<string> MessageList, string StorageFile)
        {
            //if (!File.Exists(StorageFile))
            //{
            //    File.Create(StorageFile);
            //}
            lock (Buffer)
            {
                foreach (var item in Buffer)
                {
                    Console.WriteLine(item);
                }

            }           
        }
    }
}
