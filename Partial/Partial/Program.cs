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
            Calculator c = new Calculator(10, 20, 30);
            Console.WriteLine("Sum: " + c.ComputeSum(5, 10, 15, 20));
            Console.WriteLine("Can be repared: " + c.Repair(DateTime.Now.ToBinary()));

            double[] characteristics = c.GetCharacteristics();
            Console.Write("Calculator's Height, Width & Weight: ");
            foreach (var item in characteristics)
            {
                Console.Write(item + "  ");
            }

            Console.WriteLine("\nPress any key");
            Console.ReadKey();
        }
    }
}
