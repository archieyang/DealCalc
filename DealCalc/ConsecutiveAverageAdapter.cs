﻿using System;
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

        public ConsecutiveAverageAdapter(List<SingleDayResult> data)
        {
            _data = data;
        }

        public void ForEach(Action<ChartItem> action)
        {
            
            SingleDayResult starting = null;
            double total = 0;

            for (int i = 0; i < _data.Count; ++i)
            {
                Debug.WriteLine(i +"");
                var item = _data[i];

                if (starting == null)
                {
                    starting = item;
                }

                total += item.EffectiveAmount;
                Debug.WriteLine(total);

                if ((i+1) % 5 == 0)
                {
                    Debug.WriteLine(total);
                    ChartItem chartItem = new ChartItem(starting.Date.ToShortDateString() + item.Date.ToShortDateString(), total / 5);
                    action?.Invoke(chartItem);

                    total = 0;
                    starting = null;
                  
                }
            }
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
    }
}
