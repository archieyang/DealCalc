using LiveCharts;
using LiveCharts.Definitions.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    public interface ChartySeriesFactory
    {
        ISeriesView Build(String title, ChartValues<double> values);
    }
}
