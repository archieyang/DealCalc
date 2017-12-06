using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DealCalc
{
    internal class RatioChartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _someText;
        public string SomeText
        {
            get => _someText;
            set
            {
                _someText = value;
               OnPropertyChanged("SomeText");
            }
        }

        private EffectiveRatioChart _chart;

        public EffectiveRatioChart Chart
        {
            get => _chart;
            set
            {
                _chart = value;
                OnPropertyChanged("Chart");
            }
        }

        public ICommand OpenFileCommand => new DelegateCommand(OpenFile);

        private void OpenFile()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = false,
                Filter = "Text files (*.txt)|*.txt"
            };

            if ((bool) openFileDialog.ShowDialog())
            {
                SomeText = openFileDialog.FileName;
                string[] text = System.IO.File.ReadAllLines(openFileDialog.FileName, Encoding.Default);

                string line1 = text[2];

                string[] parts = line1.Split(null);

                foreach (string s in parts)
                {
                    Debug.WriteLine(s);
                }

                Debug.WriteLine(parts.Length);

                Debug.WriteLine(TransactionData.CreateFromString(rawData: line1));

                var list = text.Select(TransactionData.CreateFromString).Where(data => data != null).ToList();

                var process = new CoreProcessor(list);

                Chart = new EffectiveRatioChart(process.Process());
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
