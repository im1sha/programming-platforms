using System;

namespace ExportClass
{
    /// <summary>
    /// This attribute stores author name and class version
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ExportClass : System.Attribute
    {
        private readonly string AuthorName;

        public double Version;

        public ExportClass(string Name)
        {
            AuthorName = Name;
            Version = 1.0;
        }
    }
}
