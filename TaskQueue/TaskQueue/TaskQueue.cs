using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskQueue
{
    // Represents a callback method to be executed by a TaskQueue thread.
    public delegate void TaskDelegate();

    public class TaskQueue
    {
        // Fields and properties section


        public int MinThreads { get; private set; } = 0;

        public int MaxThreads { get; private set; } = 0;

        public int MaxIdleTime { get; private set; } = 10;

        // Determines interval between calls of management thread
        public int ManagementInterval { get; private set; } = 100;

        // Queue of methods TaskQueue instance should process
        private Queue<TaskDelegate> WorkQueue = new Queue<TaskDelegate>();

        public int QueueLength
        {
            get
            {
                return WorkQueue.Count();
            }
        }

        // Running tasks 
        private List<WorkTask> TaskList = new List<WorkTask>();

        private Thread ManagementThread = null;

        private bool KeepManagementThreadRunning = true;


        // Constructors and deconstructors section


        public TaskQueue(int MaxWorkerThreads) : this()
        {
            SetMaxThreads(MaxWorkerThreads);
        }

        private TaskQueue()
        {
            ThreadPool.GetMinThreads(out int MinThreadsAvailable, out int MinPortsAvailable);         
            MinThreads = MinThreadsAvailable;
            if (MaxThreads == 0)
            {
                MaxThreads = MinThreads;
            }
           
            ManagementThread = new Thread(new ThreadStart(KeepManagement));
            ManagementThread.Start();
        }
      
        ~TaskQueue()
        {
            Close();
        }


        // Public methods section


        // Retrieves the difference between the maximum number of TaskQueue threads 
        // and the number currently active
        public int GetAvailableThreads()
        {
            return MaxThreads - TaskList.Count;
        }

        // Sets the number of requests to the TaskQueue that can be active concurrently.
        public bool SetMaxThreads(int workerThreads)
        {
            bool result = false;
            if (workerThreads >= 1)
            {
                ThreadPool.GetMaxThreads(out int maxThreadsAvailable, out int maxPortsAvailable);
                if (maxThreadsAvailable >= workerThreads)
                {
                    MaxThreads = workerThreads;
                    result = true;
                }
            }
            return result;
        }

        // Sets the minimum number of threads the TaskQueue creates on demand, as new requests are made, 
        // before switching to an algorithm for managing thread creation and destruction
        public bool SetMinTreads(int workerThreads)
        {
            bool result = false;
            ThreadPool.GetMaxThreads(out int maxThreadsAvailable, out int maxPortsAvailable);
            if (workerThreads <= maxThreadsAvailable)
            {
                ThreadPool.GetMinThreads(out int minThreadsAvailable, out int minPortsAvailable);
                if (minThreadsAvailable <= workerThreads)
                {
                    MinThreads = workerThreads;
                    result = true;
                }
            }
            return result;
        }

        // Sets interval between calls of management thread
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

        // Sets max time thread's allowed to be in state of Wait, Sleep or Join
        public bool SetMaxIdleTime(int seconds)
        {
            bool result = false;
            if (seconds > 0)
            {
                MaxIdleTime = seconds;
                result = true;
            };
            return result;
        }

        // Queues a method for execution. 
        // The method executes when a TaskQueue thread becomes available
        public void EnqueueTask(TaskDelegate task)
        {
            lock (WorkQueue)
            {
                WorkQueue.Enqueue(task);
            }

            bool IdleThreadExists = false;
            foreach (WorkTask t in TaskList)
            {
                if (!t.Busy)
                {
                    t.WakeUp();
                    IdleThreadExists = true;
                    break;
                }
            }

            if (!IdleThreadExists)
            {
                if (TaskList.Count < MaxThreads)
                {
                    WorkTask t = new WorkTask(ref WorkQueue);
                    lock (TaskList)
                    {
                        TaskList.Add(t);
                    }
                }
            }
        }

        // Destroys TaskQueue instance, running tasks and management thread
        public void Close()
        {
            KeepManagementThreadRunning = false;

            if (ManagementThread != null)
            {
                if (ManagementThread.ThreadState == ThreadState.WaitSleepJoin)
                {
                    ManagementThread.Interrupt();
                }
                ManagementThread.Join();
            }

            ManagementThread = null;

            foreach (WorkTask task in TaskList)
            {
                task.Close();
            }
        }


        // Private methods section


        // Keeps tracking of threads that are running more than MaxIdleTime
        private void KeepManagement()
        {
            while (KeepManagementThreadRunning)
            {
                try
                {
                    if (TaskList.Count > MinThreads)
                    {
                        foreach (WorkTask task in TaskList)
                        {
                            if (DateTime.Now.Subtract(task.LastOperation).Seconds > MaxIdleTime)
                            {
                                task.Close();
                                lock (TaskList)
                                {
                                    TaskList.Remove(task);
                                    break;
                                }
                            }
                        }
                    }

                    Thread.Sleep(ManagementInterval);
                }
                catch
                {
                }
            }
        }
    }   
}
