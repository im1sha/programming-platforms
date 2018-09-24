using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{ 
    /// <summary>
    /// Represents a callback method to be executed by a TaskQueue thread
    /// </summary>
    /// <param name="o">Any object</param>
    public delegate void TaskDelegate(object o);


    /// <summary>
    /// Class instance of which passed as parameter to TaskQueue.EnqueueTask method
    /// </summary>
    public class UnitOfWork
    {
        /// <summary>
        /// Represents method that will be run by TaskQueue
        /// </summary>
        public TaskDelegate Task { get; }

        /// <summary>
        /// Object which should be passed as parameter to TaskDelegate
        /// </summary>
        public object Obj { get; }

        /// <summary>
        /// </summary>
        /// <param name="Task">Represents method that will be run by TaskQueue</param>
        /// <param name="Obj">Object which should be passed as parameter to TaskDelegate</param>
        public UnitOfWork(TaskDelegate Task, object Obj)
        {
            this.Task = Task;
            this.Obj = Obj;
        }
    }
}
