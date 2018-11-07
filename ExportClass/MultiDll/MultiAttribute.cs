using System;

namespace MultiDll
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    public class MultiAttribute : System.Attribute
    {
        public string Data;

        public MultiAttribute(string data)
        {
            Data = data;
        }
    }
}
