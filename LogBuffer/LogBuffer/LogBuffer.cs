using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
        /// Time between writes on hard drive 
        /// </summary>
        private double ReleaseTime;

        /// <summary>
        /// Buffer capacity 
        /// </summary>
        private int BufferSize;

        private readonly double DefaultReleaseTimeInSeconds = 10;

        private readonly int DefaultBufferSize = 50;

        /// <summary>
        /// Creates imnstance of LogBuffer class
        /// </summary>
        /// <param name="BufferSize">Buffer capacity</param>
        /// <param name="ReleaseTime">Time between writes can't exceed this value</param>
        public LogBuffer(int BufferSize, double ReleaseTime, string StorageFile)
        {
            if (BufferSize > 0)
            {
                this.BufferSize = BufferSize;
            }
            if (ReleaseTime > 0)
            {
                this.ReleaseTime = ReleaseTime;
            }
            this.StorageFile = StorageFile;
        }

        /// <summary>
        /// Appends string to buffer
        /// </summary>
        /// <param name="Item">Message ot add</param>
        public void Add(string Item)
        {
            Buffer.Add(Item);
            if (BufferSize <= Buffer.Count)
            {
                AsyncWrite(Buffer, StorageFile);
            }
        }

        /// <summary>
        /// Writes messages from list to file asynchronously
        /// </summary>
        /// <param name="MessageList">List with messages</param>
        private void AsyncWrite(List<string> MessageList, string StorageFile)
        {
            if (!File.Exists(StorageFile))
            {
                File.Create(StorageFile);
            }
            throw new NotImplementedException();
        }
    }
}
