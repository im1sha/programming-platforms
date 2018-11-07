using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace MultifileAssemblyViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Path to library should be passed");
            }
            string libPath = args[0];

            TestAttributesRetrieving(libPath);
        }

        static void TestAttributesRetrieving(string libPath)
        {
            Dictionary<Type, Attribute[]> info = AttributeViewer.Viewer.RetrieveAttributes(libPath);

            foreach (KeyValuePair<Type, Attribute[]> data in info)
            {
                Console.Write(data.Key);
                foreach (Attribute attribute in data.Value)
                {
                    Console.Write("\n\t" + attribute);
                    if (attribute.GetType() == typeof(ExportDll.ExportClassAttribute))
                    {
                        Console.Write($" <{((ExportDll.ExportClassAttribute)attribute).Version}> ");
                    }
                    if (attribute.GetType() == typeof(MultiDll.MultiAttribute))
                    {
                        Console.Write($" <{((MultiDll.MultiAttribute)attribute).Data}> ");
                    }
                }
                if (data.Value.Length == 0)
                {
                    Console.Write("\n\tno attributes");
                }
                Console.WriteLine();
            }

        }
    }
}
