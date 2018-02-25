using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

namespace DealCalc
{
    class SingleDayAdapter : ChartAdapter
    {
        private readonly List<SingleDayResult> _data;

        public SingleDayAdapter(List<SingleDayResult> data)
        {
            _data = data;
        }

        public void ForEach(Action<ChartItem> action)
        {
            _data.ForEach(item =>
            {
                ChartItem chartItem = new ChartItem(item.Date.ToShortDateString(), item.EffectiveRatio);
                action?.Invoke(chartItem);
            });
        }

        public void ForEachSeries(Action<ISeriesView> action)
        {
            var values = new ChartValues<double>();

            _data.ForEach(item =>
            {
                values.Add(item.EffectiveRatio);
            });

            action?.Invoke(new ColumnSeries()
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
