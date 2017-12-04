using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using System.Globalization;
using LiveCharts;
using LiveCharts.Wpf;

namespace DealCalc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            string path = @"C:\Users\Home\Desktop\SH#600011.txt";
//            string path = @"C:\Users\Home\Desktop\SH#600010.txt";
            string[] text = System.IO.File.ReadAllLines(path, Encoding.Default);

            string line1 = text[2];

            string[] parts = line1.Split(null);

            foreach (string s in parts)
            {
                Debug.WriteLine(s);
            }

            Debug.WriteLine(parts.Length);

//            foreach (string s in text)
//            {
//                textPath.Content = s;
//            }

            Debug.WriteLine(TransactionData.CreateFromString(rawData: line1)); 

            var list = text.Select(TransactionData.CreateFromString).Where(data => data != null).ToList();

            var process = new CoreProcessor(list);

            SeriesCollection = new SeriesCollection();

            var values = new ChartValues<double>();
           
        
            var res = process.Process();
            var lables = new List<string>();
            res.ForEach(result =>
            {
                values.Add(result.EffectiveRatio * 100);
                lables.Add(result.Date.ToShortDateString());
            });

            Debug.WriteLine("resLength: "+res.Count);

            SeriesCollection.Add(new ColumnSeries()
            {
                Values = values
            });


            Labels = lables.ToArray();
     

            //adding series will update and animate the chart automatically
            //            SeriesCollection.Add(new ColumnSeries
            //            {
            //                Title = "2016",
            //                Values = new ChartValues<double> { 11, 56, 42 }
            //            });

            //also adding values updates and animates the chart automatically

            //            Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            //            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if ((bool)openFileDialog.ShowDialog())
            {
                Debug.WriteLine(openFileDialog.FileName);
                string[] text = System.IO.File.ReadAllLines(openFileDialog.FileName, Encoding.Default);

                string line1 = text[10];

                string[] parts = line1.Split(' ');

                foreach (string s in parts)
                {
                    System.Diagnostics.Debug.WriteLine(s);
                }

//                foreach (string s in text)
//                {
//                    textPath.Content = s;
//                }

            }

        }
    }
}
