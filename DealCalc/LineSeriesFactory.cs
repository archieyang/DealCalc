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
    class LineSeriesFactory : ChartySeriesFactory
    {
        public ISeriesView Build(string title, ChartValues<double> values)
        {
            return new LineSeries()
            {
                Title = title,
                Values = values
            };
        }
    }
}
