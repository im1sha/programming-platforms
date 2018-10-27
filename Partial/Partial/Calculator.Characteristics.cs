using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partial
{
    partial class Calculator
    {
        public Calculator(double height, double width, double weight)
        {
            characteristics = new Characteristics();
            characteristics.Height = height;
            characteristics.Width = width;
            characteristics.Weight = weight;
        }

        private Characteristics characteristics;

        partial struct Characteristics
        {
            public double Height, Width, Weight;
        }

        partial struct Characteristics
        {
            public double[] RetrieveCharacteristics()
            {
                return new double[] { Height, Width, Weight };
            }
        }

        public double[] GetCharacteristics()
        {
            return characteristics.RetrieveCharacteristics();
        }
    }
}
