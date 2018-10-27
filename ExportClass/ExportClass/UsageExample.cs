using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ExportClass
{
    /// <summary>
    /// Class that demonstates how to use ExportClassViewer
    /// </summary>
    class UsageExample
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        /// <param name="args">Path to library</param>
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Path to library should be passed");
            }
            string libPath = args[0];

            TestRetrieveAttributes(libPath);

            // TestRetrieveExportClasses(libPath);

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        static void TestRetrieveAttributes(string libPath)
        {
            Dictionary<Type, Attribute[]> info = AttributeViewer.RetrieveAttributes(libPath);
            foreach (var data in info)
            {
                Console.Write(data.Key + ":\n\t");
                foreach (var attribute in data.Value)
                {                    
                    Console.Write(attribute + "  ");              
                }
                Console.WriteLine();
            }
           
        }

        static void TestRetrieveExportClasses(string libPath)
        {          
            List<string> exportClasses = AttributeViewer.RetrieveExportClasses(libPath);

            if (exportClasses.Count == 0)
            {
                Console.WriteLine("No items of ExportClass type");
            }
            else
            {
                foreach (string item in exportClasses)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}

