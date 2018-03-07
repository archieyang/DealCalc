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
        private readonly Action<ChartValues<double>> _emptyDataFiller;

        public ConsecutiveAverageAdapter(List<SingleDayResult> data, int consecutiveNum) : this(data, consecutiveNum, new ColumnSeriesFactory(), values => { })
        {
            
        }


        public ConsecutiveAverageAdapter(List<SingleDayResult> data, int consecutiveNum, ChartySeriesFactory chartySeriesFactory, Action<ChartValues<double>> filler)
        {
            _data = data;
            _consecutiveNum = consecutiveNum;
            _chartySeriesFactory = chartySeriesFactory;
            _emptyDataFiller = filler;
        }

        public virtual void ForEachSeries(Action<ISeriesView> data, Action<string> labels)
        {
            var values = new ChartValues<double>();

            for (int i = 0; i < _data.Count; ++i)
            {
                if (i < _consecutiveNum - 1)
                {
                    _emptyDataFiller.Invoke(values);
                }
                else
                {
                    double total = 0;
                    for (int j = i; j >= i - _consecutiveNum + 1; --j)
                    {
                        var item = _data[j];
                        total += item.EffectiveAmount;
                    }

                    labels?.Invoke(_data[i].Date.ToShortDateString());
                    values.Add(total / _consecutiveNum);
                }

            }

            data?.Invoke(_chartySeriesFactory.Build("连续" + _consecutiveNum + "日均量", values));
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
