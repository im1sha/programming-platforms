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
           
            List<string> exportClasses = ExportClassViewer.RetrieveExportClasses(libPath);

            foreach (string item in exportClasses)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}

