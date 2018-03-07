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
        private String _step = null;
        private string[] _labels;
        private String xname = "x";
        private String yname = "y";
        private Func<double, string> _formatter;

        public String Xname
        {
            get => xname;
            set
            {
                xname = value;
                OnPropertyChanged();
            }
        }
        
        public String Yname
        {
            get => yname;
            set
            {
                yname = value;
                OnPropertyChanged();
            }
        }

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

            var lables = new List<string>();

            _adapter.ForEachSeries(series =>
            {
                seriesCollection.Add(series);
            }, label=> { lables.Add(label); });

            SeriesCollection = seriesCollection;
            Labels = lables.ToArray();

            UpperSection = _adapter.Upper();
            LowerSection = _adapter.Lower();
            Step = _adapter.Step();
            Formatter = _adapter.Formatter();

            Xname = _adapter.Xname();
            Yname = _adapter.Yname();

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

        public String Step
        {
            get => _step;
            set
            {
                _step = value;
                OnPropertyChanged();
            }
        }

        public Func<double, string> Formatter
        {
            get => _formatter;
            set
            {
                _formatter = value;
                OnPropertyChanged();
            }
        }

        public enum Type
        {
            Normal,
            Abs,
            Sum,
            ConsecutiveComposite,
            ConsecutiveAverage,
            ConsecutiveRatio
        }

    }
}
