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
        private string FilePath;

        /// <summary>
        /// Time between writes on hard drive 
        /// </summary>
        private int ReleaseTime;

        /// <summary>
        /// Buffer capacity 
        /// </summary>
        private int BufferSize;

        /// <summary>
        /// Creates imnstance of LogBuffer class
        /// </summary>
        /// <param name="MaxBufferSize">Buffer capacity</param>
        /// <param name="ReleaseTime">Time between writes can't exceed this value</param>
        public LogBuffer(int MaxBufferSize, int ReleaseTime)
        {

        }

        /// <summary>
        /// Appends string to buffer
        /// </summary>
        /// <param name="item">message ot add</param>
        public void Add(string item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes messages from list to file asynchronously
        /// </summary>
        /// <param name="list">List with messages</param>
        private void AsyncWrite(List<string> list)
        {
            throw new NotImplementedException();
        }
    }
}
