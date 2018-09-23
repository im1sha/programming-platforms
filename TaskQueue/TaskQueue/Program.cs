using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            const int queueAmount = 100;
            TaskQueue T = new TaskQueue(queueAmount);

            for (int i = 0; i < queueAmount; i++)
            {
                T.EnqueueTask(DoWork);
            }

            T.Close();

            Console.WriteLine("press any key");
            Console.ReadLine();
        }


        static private void DoWork()
        {
            Console.WriteLine("started");
            var rand = new Random();
            int total = rand.Next(100000);
            int val = 1;
            for (int j = 0; j < total; j++)
            {
                val *= 2;
            }
            Console.WriteLine("ended");
        }
    }
}
