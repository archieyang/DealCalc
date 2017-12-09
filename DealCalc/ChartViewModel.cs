using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;

namespace DealCalc
{
    internal class ChartViewModel : ViewModel
    {
        private readonly List<SingleDayResult> _data;
        private SeriesCollection _seriesCollection;
        private string[] _labels;

        public ChartViewModel(List<SingleDayResult> data)
        {
            _data = data;
            var seriesCollection = new SeriesCollection();

            var values = new ChartValues<double>();
            var lables = new List<string>();
            _data.ForEach(result =>
            {
                values.Add(result.EffectiveRatio * 100);
                lables.Add(result.Date.ToShortDateString());
            });

            Debug.WriteLine("resLength: " + _data.Count);

            seriesCollection.Add(new ColumnSeries()
            {
                Values = values
            });

            SeriesCollection = seriesCollection;
            Labels = lables.ToArray();
        }

        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set
            {
                _seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public string[] Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                OnPropertyChanged("Labels");
            }
        }

    }
}
