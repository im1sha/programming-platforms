using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutex
{
    /// <summary>
    /// A synchronization primitive
    /// </summary>
    public class Mutex
    {
        /// <summary>
        /// Synchronization variable 
        /// </summary> 
        private static int UsingResource;

        private static readonly int InUse = 1;
        private static readonly int NotUsed = 0;

        public Mutex() : this(false) { }

        /// <summary>
        /// Constructor that gets initial instance state 
        /// </summary>
        /// <param name="initiallyOwned">Initial mutex state</param>
        public Mutex(bool initiallyOwned)
        {
            UsingResource = initiallyOwned ? InUse : NotUsed;
        }

        /// <summary>
        /// Blocks the current thread until the current Mutex instance receives a signal
        /// </summary>
        public void Lock()
        {
            int initialValue;
            do
            {
                initialValue = UsingResource;               
            } while (initialValue == InUse || initialValue != Interlocked.CompareExchange(ref UsingResource, InUse, initialValue));
        }

        /// <summary>
        /// Releases a mutex
        /// </summary>
        public void Unlock()
        {
            UsingResource = NotUsed;
        }
    }
}
