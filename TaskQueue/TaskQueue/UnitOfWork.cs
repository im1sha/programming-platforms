using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    // Represents a callbackmethod to be executed by a TaskQueue thread.
    public delegate void TaskDelegate(object o);

    public class UnitOfWork
    { 
        public TaskDelegate Task { get; }

        public object Obj { get; }

        public UnitOfWork(TaskDelegate Task, object Obj)
        {
            this.Task = Task;
            this.Obj = Obj;
        }
    }
}
