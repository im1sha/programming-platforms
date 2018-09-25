using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyViewer
{
    class Program
    {
        /// <summary>
        /// Entry point of the application 
        /// </summary>
        /// <param name="args">Full path to .dll or .exe file should be passed</param>
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new Exception("Wrong parameters");
            }

            string libPath = args[0];

            if (!File.Exists(libPath))
            {
                throw new Exception("File doesn't exist");
            }

            List<string> classInfo = SortLib(libPath);

            foreach (string item in classInfo)
            {
                Console.WriteLine(item);
            }
        }


        /// <summary>
        ///  Observes library to find all public types and 
        ///  sorts them all by namespace and then by class name
        /// </summary>
        /// <param name="dllPath">Library that are used</param>
        /// <returns>Sorted class list</returns>
        public static List<string> SortLib(string dllPath)
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);
                           
            Type[] types = assembly.GetExportedTypes();             
            List<Type> requiredTypes = types.Where(type => type.IsClass).ToList();

            List<string> classInfo = new List<string>();

            foreach (Type t in requiredTypes)
            {
                classInfo.Add(t.FullName);
            }
            classInfo.Sort((s1, s2) => string.Compare(s1, s2));

            return classInfo;
        }      
    }
}

