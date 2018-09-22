using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    // Represents a callback method to be executed by a TaskQueue thread.
    public delegate void TaskDelegate();

    public class TaskQueue
    {


        // Fields section



        // Constructors and deconstructors section


        public TaskQueue(int workerThreads) : this() { }

        public TaskQueue() { }

        ~TaskQueue() { }


        // Public methods section


        // Retrieves the difference between the maximum number of TaskQueue threads 
        // and the number currently active
        public int GetAvailableThreads()
        {
            throw new NotImplementedException();
        }

        // Retrieves the number of requests to the TaskQueue that can be active concurrently
        public int GetMaxThreads()
        {
            throw new NotImplementedException();
        }

        // Sets the number of requests to the TaskQueue that can be active concurrently.
        public bool SetMaxThreads(int workerThreads)
        {
            throw new NotImplementedException();
        }

        // Retrieves the minimum number of threads the TaskQueue creates on demand, as new requests are made, 
        // before switching to an algorithm for managing thread creation and destruction
        public int GetMinTreads() {
            throw new NotImplementedException();

        }

        // Sets the minimum number of threads the TaskQueue creates on demand, as new requests are made, 
        // before switching to an algorithm for managing thread creation and destruction
        public bool SetMinTreads()
        {
            throw new NotImplementedException();

        }

        // Queues a method for execution. 
        // The method executes when a TaskQueue thread becomes available
        public void EnqueueTask(TaskDelegate task)
        {
            throw new NotImplementedException();
        }


        // Private methods section


    }
}
