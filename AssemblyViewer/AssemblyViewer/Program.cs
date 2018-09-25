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
        static void Main(string[] args)
        {
            string dllName = "ClassLibrary.dll";
            string currentDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string dllPath = Path.Combine(currentDir, dllName);

            if (!File.Exists(dllPath))
            {
                throw new Exception("File doesn't exist");
            }

            PrintClasses(dllPath);
        }

        /// <summary>
        /// Observes library to find all public types and 
        /// prints them all using cort by namespace and then by class name
        /// </summary>
        /// <param name="dllPath">Library that are used</param>
        public static void PrintClasses(string dllPath)
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);
                           
            Type[] types = assembly.GetExportedTypes();             
            List<Type> requiredTypes = types.Where(type => type.IsClass).ToList();

            List<string> classInfo = new List<string>();

            foreach (var item in requiredTypes)
            {
                classInfo.Add(item.FullName);
            }

            classInfo.Sort((s1, s2) => string.Compare(s1, s2));

            foreach (var item in classInfo)
            {
                Console.WriteLine(item);
            }                  
        }      
    }
}

