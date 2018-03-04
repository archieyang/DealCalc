using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

namespace DealCalc
{
    public class ConsecutiveAverageAdapter : ChartAdapter
    {
        protected readonly List<SingleDayResult> _data;
        protected readonly int _consecutiveNum;
        private readonly ChartySeriesFactory _chartySeriesFactory;

        public ConsecutiveAverageAdapter(List<SingleDayResult> data, int consecutiveNum) : this(data, consecutiveNum, new ColumnSeriesFactory())
        {
            
        }


        public ConsecutiveAverageAdapter(List<SingleDayResult> data, int consecutiveNum, ChartySeriesFactory chartySeriesFactory)
        {
            _data = data;
            _consecutiveNum = consecutiveNum;
            _chartySeriesFactory = chartySeriesFactory;
        }

        public virtual void ForEach(Action<ChartItem> action)
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

        public virtual void ForEachSeries(Action<ISeriesView> action)
        {
            var values = new ChartValues<double>();

            for (int i = 0; i < _data.Count; ++i)
            {
                double total = 0;
                for (int j = i; j < _data.Count; ++j)
                {
                    var item = _data[j];
                    total += item.EffectiveAmount;

                    if ((j - i + 1) == _consecutiveNum)
                    {
                        values.Add(total / _consecutiveNum);
                    }
                }
            }

            action?.Invoke(_chartySeriesFactory.Build("连续" + _consecutiveNum + "日均量", values));
        }

        public Func<double, string> Formatter()
        {
            return d => $"{d / 10000:0.00}万";
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
            return "日期";
        }

        public string Yname()
        {
            return "有效交易量";
        }
    }
}
