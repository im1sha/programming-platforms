using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// WARNING! .NET Core 2.0.0 required 
/// https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.0-download.md 
/// </summary>
namespace AttributeViewer
{
    public static class Viewer
    {
        /// <summary>
        /// Retrieves all classes marked with ExportClass attribute
        /// </summary>
        /// <param name="dllPath">Path to library where to search</param>
        /// <returns>List of class names</returns>
        public static List<string> RetrieveExportClasses(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                throw new Exception("Library doesn't exist");
            }

            Assembly assembly = Assembly.LoadFrom(dllPath);

            Type[] types = assembly.GetExportedTypes();

            List<Type> requiredTypes = (types != null)
                ? types.Where(type => {
                    try
                    {
                        return Attribute.GetCustomAttribute(type, typeof(ExportDll.ExportClassAttribute)) != null;
                    }
                    catch
                    {
                        return false;
                    }
                }).ToList()
                : new List<Type>();

            List<string> classInfo = new List<string>();

            foreach (Type t in requiredTypes)
            {
                classInfo.Add(t.FullName);
            }
            classInfo.Sort((s1, s2) => string.Compare(s1, s2));

            return classInfo;
        }


        /// <summary>
        /// Retrieves attributes to all the classes from assembly passed by user
        /// </summary>
        /// <param name="dllPath">Path to library where to search</param>
        /// <returns>Dictionary with classes and related attributes</returns>
        public static Dictionary<Type, Attribute[]> RetrieveAttributes(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                throw new Exception("Library doesn't exist");
            }

            Assembly assembly = Assembly.LoadFrom(dllPath);

            Type[] types = assembly.GetTypes();

            var result = new Dictionary<Type, Attribute[]>();

            foreach (Type t in types)
            {
                Attribute[] attributes = Attribute.GetCustomAttributes(t);
                result.Add(t, attributes);
            }

            return result;
        }
    }
}
