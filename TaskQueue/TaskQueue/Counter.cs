using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    /// <summary>
    /// Class used to store number of performed operations 
    /// </summary>
    public class Counter
    {
        public int Total { get; private set; } = 0;

        /// <summary>
        /// Increments stored value
        /// </summary>        
        public void Increment()
        {
            Total++;
        }
    }
}
