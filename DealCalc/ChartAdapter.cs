using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    interface ChartAdapter
    {
        void ForEach(Action<ChartItem> action);
        double Upper();
        double Lower();
        double Step();
    }
}
