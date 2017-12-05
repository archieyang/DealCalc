using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace DealCalc
{
    /// <summary>
    /// EffectiveRatioChart.xaml 的交互逻辑
    /// </summary>
    public partial class EffectiveRatioChart : UserControl
    {
        public EffectiveRatioChart(List<SingleDayResult> res)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection();

            var values = new ChartValues<double>();
            var lables = new List<string>();
            res.ForEach(result =>
            {
                values.Add(result.EffectiveRatio * 100);
                lables.Add(result.Date.ToShortDateString());
            });

            Debug.WriteLine("resLength: " + res.Count);

            SeriesCollection.Add(new ColumnSeries()
            {
                Values = values
            });

            Labels = lables.ToArray();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
