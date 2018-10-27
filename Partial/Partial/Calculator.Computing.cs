using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partial
{
    partial class Calculator
    {
        public void CallWorkMethod()
        {
            DoWork();
        }

        partial void DoWork();

        public int ComputeSum(params int[] p)
        {
            return p.Sum();
        }
    }
}
