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
        private  double _upperSection = 0.05;
        private double _lowerSection = -0.05;
        private double _step = 0.05;
        private string[] _labels;

        private Type _type;

        public ChartViewModel(List<SingleDayResult> data, Type defaultType)
        {
            _data = data;
            ChartType = defaultType;
            Formatter = d => $"{d:P2}.";
        }

        public Type ChartType
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged("ChartType");

                var seriesCollection = new SeriesCollection();

                var values = new ChartValues<double>();
                var lables = new List<string>();
                _data.ForEach(result =>
                {
                    values.Add( value ==Type.Normal ? result.EffectiveRatio :result.AbsEffectiveRatio);
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

        public double UpperSection
        {
            get => _upperSection;
            set
            {
                _upperSection = value;
                OnPropertyChanged("UpperSection");
            }
        }

        public double LowerSection
        {
            get => _lowerSection;
            set
            {
                _lowerSection = value;
                OnPropertyChanged("LowerSection");
            }
        }

        public double Step
        {
            get => _step;
            set
            {
                _step = value;
                OnPropertyChanged("Step");
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
