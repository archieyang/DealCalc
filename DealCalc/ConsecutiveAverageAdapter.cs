using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    class ConsecutiveAverageAdapter : ChartAdapter
    {
        private readonly List<SingleDayResult> _data;
        private readonly int _consecutiveNum;

        public ConsecutiveAverageAdapter(List<SingleDayResult> data, int consecutiveNum)
        {
            _data = data;
            _consecutiveNum = consecutiveNum;
        }

        public void ForEach(Action<ChartItem> action)
        {
            for (int i = 0; i < _data.Count; ++i)
            {
                double total = 0;
                for (int j = i; j < _data.Count; ++j)
                {
                    var item = _data[j];
                    total += item.EffectiveAmount;

                    if ((j - i +1) ==_consecutiveNum)
                    {
                        ChartItem chartItem = new ChartItem(_data[i].Date.ToShortDateString() + "-" + _data[j].Date.ToShortDateString(), total / _consecutiveNum);
                        action?.Invoke(chartItem);
                    }
                }
            }
        }

        public Func<double, string> Formatter()
        {
            return d => $"{d:0.00}";
        }

        public double Lower()
        {
            return 0;
        }

        public double Step()
        {
            return 1000000;
        }

        public double Upper()
        {
            return 0;
        }

        public string Xname()
        {
            return "日期";
        }

        public string Yname()
        {
            return "有效交易额";
        }
    }
}
