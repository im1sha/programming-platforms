using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

            TestRetrieveAttributes(libPath);
        }

        static void TestRetrieveAttributes(string libPath)
        {
            Dictionary<Type, Attribute[]> info = ExportClass.AttributeViewer.RetrieveAttributes(libPath);
            foreach (KeyValuePair<Type, Attribute[]> data in info)
            {
                Console.Write(data.Key);
                foreach (Attribute attribute in data.Value)
                {
                    Console.Write("\n\t" + attribute);
                    if (attribute.GetType() == typeof(ExportClass.ExportClassAttribute))
                    {
                        Console.Write($" <{((ExportClass.ExportClassAttribute)attribute).Version}> ");
                    }
                    if (attribute.GetType() == typeof(ExportClass.MultiAttribute))
                    {
                        Console.Write($" <{((ExportClass.MultiAttribute)attribute).Data}> ");
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
