using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;

namespace DealCalc
{
    internal class ChartViewModel : ViewModel
    {
        private SeriesCollection _seriesCollection;
        private ChartAdapter _adapter = new EmptyAdapter();
        private  double _upperSection = 0.05;
        private double _lowerSection = -0.05;
        private double _step = 0.05;
        private string[] _labels;

   

        public ChartViewModel()
        {
            Formatter = d => $"{d:P2}.";
        }

        public ChartAdapter Adapter
        {
            get => _adapter;
            set
            {
                _adapter = value;
                Refresh();
            }

        }

        private void Refresh()
        {
            var seriesCollection = new SeriesCollection();

            var values = new ChartValues<double>();
            var lables = new List<string>();
            _adapter.ForEach(result =>
            {
                values.Add(result.value);
                lables.Add(result.label);
            });

            seriesCollection.Add(new ColumnSeries()
            {
                Title = "百分比",
                Values = values
            });

            SeriesCollection = seriesCollection;
            Labels = lables.ToArray();

            UpperSection = _adapter.Upper();
            LowerSection = _adapter.Lower();
            Step = _adapter.Step();

        }

        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public string[] Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                OnPropertyChanged();
            }
        }

        public double UpperSection
        {
            get => _upperSection;
            set
            {
                _upperSection = value;
                OnPropertyChanged();
            }
        }

        public double LowerSection
        {
            get => _lowerSection;
            set
            {
                _lowerSection = value;
                OnPropertyChanged();
            }
        }

        public double Step
        {
            get => _step;
            set
            {
                _step = value;
                OnPropertyChanged();
            }
        }

        public Func<double, string> Formatter { get; set; }

        public enum Type
        {
            Normal,
            Abs,
            FIVE_DAY
        }

    }
}
