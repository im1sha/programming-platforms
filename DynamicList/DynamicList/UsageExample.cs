using System;

namespace DynamicList
{
    class UsageExample
    {
        static void Main(string[] args)
        {
            // DynamicList creation
            DynamicList<int> L = new DynamicList<int>() { 45, 46, 47 };


            // Add usage
            for (int i = 1; i < 11; i++)
            {
                L.Add(i);
            }
            for (int i = 0; i < L.Count; i++)
            {
                Console.WriteLine(L[i]);
            }
            Console.WriteLine($"Count {L.Count}");
            Console.WriteLine($"Capacity {L.Capacity}");
            Console.WriteLine("===========================");


            // Remove usage
            L.Remove(2);
            L.Remove(9);
            L.Remove(11);           
            foreach (var item in L)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Count {L.Count}");
            Console.WriteLine($"Capacity {L.Capacity}");
            Console.WriteLine("===========================");


            // RemoveAt usage
            L.RemoveAt(1);
            L.RemoveAt(5);            
            foreach (var item in L)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Count {L.Count}");
            Console.WriteLine($"Capacity {L.Capacity}");
            Console.WriteLine("===========================");


            // Clear usage
            L.Clear();           
            foreach (var item in L)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Count {L.Count}");
            Console.WriteLine($"Capacity {L.Capacity}");
            Console.WriteLine("===========================");
            Console.WriteLine("Press Any Key");
            Console.ReadKey();
        }
    }
}
