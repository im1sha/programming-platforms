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


        /// <summary>
        /// Link to queue from TaskQueue class
        /// </summary>
        private Queue<UnitOfWork> WorkQueue = null;

        /// <summary>
        /// Current process
        /// </summary>
        private Thread WorkProcess = null;

        /// <summary>
        /// Determines whether the thread should continue running
        /// </summary>
        private bool KeepRunning = true;

        public bool Busy { get; private set; } = false;

        public DateTime LastOperation { get; private set; } = DateTime.Now;

        public int ManagementInterval { get; private set; } = 100;


        // constructs section


        public WorkTask(ref Queue<UnitOfWork> WorkQueue)
        {
            this.WorkQueue = WorkQueue;
            WorkProcess = new Thread(ExecutePassedMethod);
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


        /// <summary>
        /// Sets interval between checking for not processed tasks
        /// </summary>
        /// <param name="millisecondsTimeout">Desired interval between calls of management method</param>
        /// <returns>Operation success</returns>
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

        /// <summary>
        /// Destroys executed task
        /// </summary>
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

        /// <summary>
        /// Interupts idling thread
        /// </summary>
        internal void WakeUp()
        {
            if (WorkProcess.ThreadState == ThreadState.WaitSleepJoin)
            {
                WorkProcess.Interrupt();
            }
            Busy = true;
        }


        // private methods


        /// <summary>
        /// Executes task belongs to queue of all the passed to TaskQueue delegates
        /// </summary>
        private void ExecutePassedMethod()
        {
            UnitOfWork u;

            while (KeepRunning)
            {
                try
                {
                    while (WorkQueue.Count > 0)
                    {
                        u = null;

                        lock (WorkQueue)
                        {
                            u = WorkQueue.Dequeue();
                        }

                        if (u != null && u.Task != null)
                        {
                            LastOperation = DateTime.Now;
                            Busy = true;
                            u.Task(u.Obj);
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
