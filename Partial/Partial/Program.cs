using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partial
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            Console.WriteLine("Sum: " + c.ComputeSum(5, 10, 15, 20));
            Console.WriteLine("Can be repared: " + c.Repair(DateTime.Now.ToBinary()));

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
