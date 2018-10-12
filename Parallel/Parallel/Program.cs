using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel
{
    class Program
    {
        static void Main(string[] args)
        {
            WaitAllMethodExample();
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        static void WaitAllMethodExample()
        {
            List<Action<object>> actions = new List<Action<object>>();
            List<object> objects = new List<object>();
            for (int i = 0; i < 20; i++)
            {
                Action<object> a = (o) => { Thread.Sleep(500); Console.WriteLine((int)o); };
                actions.Add(a);
                objects.Add(i);
            }
            Parallel.WaitAll(actions.ToArray(), objects.ToArray());
        } 
    }
}
