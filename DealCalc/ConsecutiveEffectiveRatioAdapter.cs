using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;

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

        public void ForEachSeries(Action<ISeriesView> data, Action<string> labels)
        {
            var values = new ChartValues<double>();

            for (int i = 0; i < _data.Count; ++i)
            {
                double effectiveAmount = 0;
                double totalAmount = 0;

                for (int j = i; j < _data.Count; ++j)
                {
                    var item = _data[j];

                    effectiveAmount += item.EffectiveAmount;
                    totalAmount += item.TotalAmount;

                    if ((j - i + 1) == _consecutiveNum)
                    {
                        values.Add(effectiveAmount / totalAmount);
                        labels?.Invoke(_data[j].Date.ToShortDateString());
                    }
                }
            }

            data?.Invoke(new ColumnSeries()
            {
                Title = Yname(),
                Values = values
            });
        }

        public Func<double, string> Formatter()
        {
            return d => $"{d:P2}.";
        }

        public double Lower()
        {
            return -0.05;
        }
        
        public String Step()
        {
            return "0.05";
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
