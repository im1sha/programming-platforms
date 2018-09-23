using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskQueue
{
    class WorkTask
    {
        // Fields and properties section


        // link to queue from TaskQueue class
        private Queue<TaskDelegate> WorkQueue = null;

        // current process
        private Thread WorkProcess = null;

        // determine whether the thread should continue running
        private bool KeepRunning = true;

        public bool Busy { get; private set; } = false;

        public DateTime LastOperation { get; private set; } = DateTime.Now;

        public int ManagementInterval { get; private set; } = 100;


        // constructs section


        public WorkTask(ref Queue<TaskDelegate> WorkQueue)
        {
            this.WorkQueue = WorkQueue;
            WorkProcess = new Thread(new ThreadStart(ExecutePassedMethod));
            WorkProcess.Start();
        }

        ~WorkTask()
        {
            if (WorkProcess != null)
            {
                Busy = false;
                KeepRunning = false;
                if (WorkProcess.ThreadState == ThreadState.WaitSleepJoin)
                {
                    WorkProcess.Interrupt();
                }
                WorkProcess.Join();
            }
        }


        // public methods


        public bool SetManagementInterval(int millisecondsTimeout)
        {
            bool result = false;
            if (millisecondsTimeout > 0)
            {
                ManagementInterval = millisecondsTimeout;
                result = true;
            };
            return result;
        }

        internal void Close()
        {
            KeepRunning = false;
            if (WorkProcess != null)
            {
                if (WorkProcess.ThreadState == ThreadState.WaitSleepJoin)
                {
                    WorkProcess.Interrupt();
                }
                WorkProcess.Join();
                WorkProcess = null;
            }        
        }

        internal void WakeUp()
        {
            if (WorkProcess.ThreadState == ThreadState.WaitSleepJoin)
            {
                WorkProcess.Interrupt();
            }
            Busy = true;
        }


        // private methods


        private void ExecutePassedMethod()
        {
            TaskDelegate del;

            while (KeepRunning)
            {
                try
                {
                    while (WorkQueue.Count > 0)
                    {
                        del = null;

                        lock (WorkQueue)
                        {
                            del = WorkQueue.Dequeue();
                        }

                        if (del != null)
                        {
                            LastOperation = DateTime.Now;
                            Busy = true;
                            del();
                        }
                    }

                    Busy = false;
                    Thread.Sleep(ManagementInterval);
                }
                catch
                {
                }
            }
        }
    }
}
