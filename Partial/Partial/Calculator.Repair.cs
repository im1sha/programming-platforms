using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partial
{
    partial class Calculator
    {
        public bool Repair(long binaryTime)
        {
            return binaryTime % 2 == 0;
        }
    }
}
