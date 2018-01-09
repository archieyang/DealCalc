﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    class SingleDayAbsoluteAdapter : ChartAdapter
    {
        private readonly List<SingleDayResult> _data;

        public SingleDayAbsoluteAdapter(List<SingleDayResult> data)
        {
            _data = data;
        }

        public void ForEach(Action<ChartItem> action)
        {
            _data.ForEach(item =>
            {
                ChartItem chartItem = new ChartItem(item.Date.ToShortDateString(), item.AbsEffectiveRatio);
                action?.Invoke(chartItem);
            });
        }

        public double Lower()
        {
            return 0.25;
        }

        public double Step()
        {
            return 0.25;
        }

        public double Upper()
        {
            return 0.25;
        }
    }
}