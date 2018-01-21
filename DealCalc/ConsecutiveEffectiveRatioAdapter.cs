﻿using System;
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
            for (int i = 0; i < _data.Count; ++i)
            {
                double effectiveAmount = 0;
                double totalAmount = 0;

                for (int j= i; j < _data.Count; ++j)
                {
                    var item = _data[j];

                    effectiveAmount += item.EffectiveAmount;
                    totalAmount += item.TotalAmount;

                    if ( (j -i +1) == _consecutiveNum)
                    {
                        ChartItem chartItem = new ChartItem(_data[i].Date.ToShortDateString() + "-" + _data[j].Date.ToShortDateString(), effectiveAmount / totalAmount);
                        action?.Invoke(chartItem);
                    }
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
