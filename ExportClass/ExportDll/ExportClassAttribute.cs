using System;

namespace ExportDll
{
    /// <summary>
    /// This attribute stores author name and class version
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ExportClassAttribute : System.Attribute
    {
        private readonly string AuthorName;

        public double Version;

        public ExportClassAttribute(string name)
        {
            AuthorName = name;
            Version = 1.0;
        }
    }
}
