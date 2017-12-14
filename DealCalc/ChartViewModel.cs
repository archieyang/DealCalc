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
        private List<SingleDayResult> _data;
        private SeriesCollection _seriesCollection;
        private  double _upperSection = 0.05;
        private double _lowerSection = -0.05;
        private double _step = 0.05;
        private string[] _labels;

        private Type _type;

        public ChartViewModel()
        {
            Formatter = d => $"{d:P2}.";
        }

        public List<SingleDayResult> Data
        {
            get => _data;
            set
            {
                _data = value;
                Refresh();
            }
        }

        public Type ChartType
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private void Refresh()
        {
            var seriesCollection = new SeriesCollection();

            var values = new ChartValues<double>();
            var lables = new List<string>();
            _data.ForEach(result =>
            {
                values.Add(ChartType == Type.Normal ? result.EffectiveRatio : result.AbsEffectiveRatio);
                lables.Add(result.Date.ToShortDateString());
            });

            Debug.WriteLine("resLength: " + _data.Count);

            seriesCollection.Add(new ColumnSeries()
            {
                Title = "百分比",
                Values = values
            });

            SeriesCollection = seriesCollection;
            Labels = lables.ToArray();

            if (ChartType.Equals(Type.Abs))
            {
                UpperSection = 0.25;
                LowerSection = 0.25;
                Step = 0.25;
            }
            else
            {
                UpperSection = 0.05;
                LowerSection = -0.05;
                Step = 0.05;
            }
        }

        public ICommand CheckAbsCommand => new DelegateCommand(SetAbs);
        public ICommand CheckNormalCommand => new DelegateCommand(SetNormal);

        private void SetNormal()
        {
            ChartType = ChartViewModel.Type.Normal;
        }
        private void SetAbs()
        {
            ChartType = ChartViewModel.Type.Abs;
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
            Abs
        }

    }
}
