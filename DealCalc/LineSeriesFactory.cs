using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

namespace DealCalc
{
    class LineSeriesFactory : ChartySeriesFactory
    {
        private readonly BrushConverter brushConverter = new BrushConverter();

        private int index = 0;
        private string[] colorValues = { "888888", "00ff00", "ff0000", "ffff00" };

        public ISeriesView Build(string title, ChartValues<double> values)
        {

            LineSeries lineSeries =  new LineSeries()
            {
                Title = title,
                Values = values,
                Stroke = (SolidColorBrush)brushConverter.ConvertFrom("#ff" + colorValues[index % colorValues.Length]),
                Fill = (SolidColorBrush)brushConverter.ConvertFrom("#33" + colorValues[index % colorValues.Length])
            };

            index = (index + 1) % colorValues.Length;

            return lineSeries;
        }
    }
}
