using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    class ConsecutiveEffectiveRatioAdapter : ChartAdapter
    {
        private readonly List<SingleDayResult> _data;
        private readonly int _consecutiveNum;

        public ConsecutiveEffectiveRatioAdapter(List<SingleDayResult> data, int consecutiveNum)
        {
            _data = data;
            _consecutiveNum = consecutiveNum;
        }


        public void ForEach(Action<ChartItem> action)
        {
            SingleDayResult starting = null;
            double effectiveAmount = 0;
            double totalAmount = 0;

            for (int i = 0; i < _data.Count; ++i)
            {
                var item = _data[i];

                if (starting == null)
                {
                    starting = item;
                }

                effectiveAmount += item.EffectiveAmount;
                totalAmount += item.TotalAmount;

                if ((i+1) % _consecutiveNum == 0)
                {

                    ChartItem chartItem = new ChartItem(starting.Date.ToShortDateString() + "-" + item.Date.ToShortDateString(), effectiveAmount / totalAmount);
                    action?.Invoke(chartItem);

                    starting = null;
                    effectiveAmount = 0;
                    totalAmount = 0;

                }

            }
        }

        public Func<double, string> Formatter()
        {
            return d => $"{d:P2}.";
        }

        public double Lower()
        {
            return -0.05;
        }

        public double Step()
        {
            return 0.05;
        }

        public double Upper()
        {
            return 0.05;
        }

        public string Xname()
        {
            return "日期";
        }

        public string Yname()
        {
            return "百分比";
        }
    }
}
