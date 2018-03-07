using LiveCharts;
using LiveCharts.Definitions.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    class ConsecutiveEffectiveSumAdapter : ChartAdapter
    {
        protected readonly List<SingleDayResult> _data;
        private readonly ChartySeriesFactory _chartySeriesFactory = new ColumnSeriesFactory();

        public ConsecutiveEffectiveSumAdapter(List<SingleDayResult> data)
        {
            _data = data;
        }

        public void ForEachSeries(Action<ISeriesView> data, Action<string> labels)
        {
            var values = new ChartValues<double>();
            double total = 0;

            for (int i = 0; i < _data.Count; ++i)
            {
                total += _data[i].EffectiveAmount;
                values.Add(total);
                labels?.Invoke(_data[i].Date.ToShortDateString());
            }

            data?.Invoke(_chartySeriesFactory.Build("总量", values));
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
            return "有效交易量之和";
        }
    }
}
