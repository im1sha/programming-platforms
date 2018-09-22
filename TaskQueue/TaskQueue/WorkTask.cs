using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    class WorkTask
    {

        // Fields and properties section


        public bool Busy { get; private set; }

        public DateTime LastOperation { get; private set; }

        private Queue<TaskDelegate> workQueue;



        // constructs section


        public WorkTask(ref Queue<TaskDelegate> workQueue)
        {
            this.workQueue = workQueue;
        }

        

        // public methods


        internal void Close()
        {
            throw new NotImplementedException();
        }

        internal void WakeUp()
        {
            throw new NotImplementedException();
        }
    }
}
