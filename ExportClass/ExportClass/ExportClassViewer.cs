using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExportClass
{
    public static class ExportClassViewer
    {
        /// <summary>
        /// Retrieves all classes marked with ExportClass attribute
        /// </summary>
        /// <param name="DllPath">Path to library where to search</param>
        /// <returns>List of class names</returns>
        public static List<string> RetrieveExportClasses(string DllPath)
        {
            if (!File.Exists(DllPath))
            {
                throw new Exception("Library doesn't exist");
            }

            Assembly assembly = Assembly.LoadFrom(DllPath);

            Type[] types = assembly.GetExportedTypes();

            List<Type> requiredTypes = types.Where(
                type => {
                    try
                    {
                        return Attribute.GetCustomAttribute(type, typeof(ExportClass)) != null;
                    }
                    catch 
                    {
                        return false;
                    }                   
                }).ToList();

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
