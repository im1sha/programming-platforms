using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    class EntryPoint
    {
        /// <summary>
        /// Entry point of program
        /// </summary>
        /// <param name="args">String representation of source, destination, max queues use</param>
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("wrong parameters");
                return;
            }

            bool successfulParsing = int.TryParse(args[2], out int queueAmount);
            if (!successfulParsing || queueAmount < 1)
            {
                Console.WriteLine("wrong 3rd parameter");
                return;
            }

            Console.WriteLine("Started");

            string source = args[0];
            string dest = args[1];

            const bool copySubs = true;
            const bool overwrite = true;
            var returnedValue = new Counter();

            var workQueue = new TaskQueue(queueAmount);
            var fm = new FileManager(workQueue);
            fm.DirectoryCopy(source, dest, copySubs, overwrite, returnedValue);
            workQueue.Close();

            Console.WriteLine($"READY. \nwas copied: {returnedValue.Total}. \nPress any key to exit...");
            Console.ReadLine();
        }
    }
}
