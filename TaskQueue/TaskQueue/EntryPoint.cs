using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            //if (args.Length != 2)
            //{
            //    Console.WriteLine("wrong parameters");
            //    return;
            //}

            const int queueAmount = 10;
            const string source = @"D:\Foxx Music";
            const string dest = @"C:\Users\Foxx\Desktop\music";
            const bool copySubs = true;
            const bool overwrite = false;

            var workQueue = new TaskQueue(queueAmount);
            var fm = new FileManager(workQueue);
            fm.DirectoryCopy(source, dest, copySubs, overwrite);
            workQueue.Close();

            Console.WriteLine("READY. Press any key to exit...");
            Console.ReadLine();
        }
    }
}
