using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Partial
{
    partial class Calculator
    {
        partial void DoWork()
        {           
            Console.WriteLine("Some calcucaltion here");
            Thread.Sleep(500);
        }
    }
}
