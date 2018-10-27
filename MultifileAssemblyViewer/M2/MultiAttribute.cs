using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportClass
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
