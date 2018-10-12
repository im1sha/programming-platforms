using System;
using System.Collections;
using System.Collections.Generic;

namespace DynamicList
{
    class DynamicEnumerator<T> : IEnumerator<T>
    {
        /// <summary>
        /// List items
        /// </summary>
        private readonly T[] Items;

        /// <summary>
        /// Start position
        /// </summary>
        private int Position = -1;

        /// <summary>
        /// Items that used by user
        /// </summary>
        private readonly int ItemsInUse;

        /// <summary>
        /// Constructor of DynamicEnumerator, class that used together with DynamicList
        /// </summary>
        /// <param name="Items">Array to process</param>
        /// <param name="ItemsInUse">Used items</param>
        public DynamicEnumerator(T[] Items, int ItemsInUse)
        {
            this.ItemsInUse = ItemsInUse;
            this.Items = Items;
        }

        /// <summary>
        /// Determines whether next item exists
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            Position++;
            return (Position < ItemsInUse);
        }

        /// <summary>
        /// Resets position to its default value
        /// </summary>
        public void Reset()
        {
            Position = -1;
        }

        /// <summary>
        /// Gets current value of list
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        /// <summary>
        /// Takes attempt to retrieve item by Position value
        /// </summary>
        public T Current
        {
            get
            {
                try
                {
                    if (Position <= ItemsInUse)
                    {
                        return Items[Position];
                    }
                    throw new InvalidOperationException();
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Value to detect redundant calls
        /// </summary>
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
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
        // ~DynamicEnumerator() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }
}
