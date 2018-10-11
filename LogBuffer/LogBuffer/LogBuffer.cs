using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogBuffer
{
    class LogBuffer:IDisposable
    {
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
                    WriteMessagesAsync();
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

        //CancellationTokenSource WriteTokenSource = new CancellationTokenSource();
        //CancellationToken WriteToken;

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
            //WriteToken = WriteTokenSource.Token;


            //Thread thready = new Thread(BackgroundPeriodicWrite);
            //thready.IsBackground = true;
            //thready.Start();

            Task backgroundPeriodicWrite = new Task(BackgroundPeriodicWrite/*, WriteToken*/);
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
        /// Writes buffer list to file asyncronously 
        /// </summary>
        /// <returns></returns>
        private Task AsyncWriteTask()
        {
            List<string> bufferToWrite = Buffer;
            Buffer = new List<string>();
            return Task.Run(() =>
            {
                lock (StorageFile)
                {
                    File.AppendAllLines(StorageFile, bufferToWrite);
                }
            });
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Thread.Sleep(1000);
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LogBuffer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
