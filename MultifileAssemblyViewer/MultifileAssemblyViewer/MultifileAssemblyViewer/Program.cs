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
            myClient.Client client = new myClient.Client();
            client.ClientMethod();

            myStringer.Stringer stringer = new myStringer.Stringer();
            stringer.StringerMethod();

        }
    }
}
