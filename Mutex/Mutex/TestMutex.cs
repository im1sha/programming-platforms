using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutex
{
    class TestMutex
    {
        private static Mutex Mut = new Mutex();
        private const int TotalIterations = 3;
        private const int TotalThreads = 10;

        static void Main(string[] args)
        {
            for (int i = 0; i < TotalThreads; i++)
            {
                Thread newThread = new Thread(new ThreadStart(ThreadProc))
                {
                    Name = string.Format("Thread {0}", i + 1)
                };
                newThread.Start();
            }
        }

        private static void ThreadProc()
        {
            for (int i = 0; i < TotalIterations; i++)
            {
                DoSomething();
            }
        }

        private static void DoSomething()
        {
            Console.WriteLine("{0} sent request", Thread.CurrentThread.Name);

            Mut.Lock();
            Console.WriteLine("{0} is in the protected code scope", Thread.CurrentThread.Name);

            Thread.Sleep(50);

            Console.WriteLine("{0} is out of the protected code scope", Thread.CurrentThread.Name);
            Mut.Unlock();
        }
    }
}
