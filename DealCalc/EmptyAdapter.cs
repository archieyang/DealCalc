using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    class EmptyAdapter : ChartAdapter
    {
        public void ForEach(Action<ChartItem> action)
        {
            
        }

        public double Lower()
        {
            return 0;
        }

        public double Step()
        {
            return 0;
        }

        public double Upper()
        {
            return 0;
        }
    }
}
