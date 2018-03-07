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
    class SingleDayAbsoluteAdapter : ChartAdapter
    {
        private readonly List<SingleDayResult> _data;

        public SingleDayAbsoluteAdapter(List<SingleDayResult> data)
        {
            _data = data;
        }

        public void ForEachSeries(Action<ISeriesView> data, Action<string> labels)
        {
            var values = new ChartValues<double>();

            _data.ForEach(item =>
            {
                values.Add(item.AbsEffectiveRatio);
                labels.Invoke(item.Date.ToShortDateString());
            });

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
            return 0.25;
        }

        public String Step()
        {
            return "0.25";
        }
        
        public double Upper()
        {
            return 0.25;
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
