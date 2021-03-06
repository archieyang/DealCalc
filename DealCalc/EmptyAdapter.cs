﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.Definitions.Series;

namespace DealCalc
{
    class EmptyAdapter : ChartAdapter
    {
        public void ForEach(Action<ChartItem> action)
        {
            
        }

        public void ForEachSeries(Action<ISeriesView> data, Action<string> labels)
        {
           
        }

        public Func<double, string> Formatter()
        {
            return d => $"{d:P2}.";
        }

        public double Lower()
        {
            return 0;
        }

        public String Step()
        {
            return null;
        }

        public double Upper()
        {
            return 0;
        }

        public string Xname()
        {
            return "x";
        }

        public string Yname()
        {
            return "y";
        }
    }
}
