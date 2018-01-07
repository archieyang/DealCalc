using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    class ChartItem
    {
        public readonly String label;
        public readonly double value;

        public ChartItem(String l, double v)
        {
            label = l;
            value = v;
        }
    }
}
