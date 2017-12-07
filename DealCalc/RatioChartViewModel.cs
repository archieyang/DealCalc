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
    internal class RatioChartViewModel : ViewModel
    {
        private string _filePathText;
        public string FilePathText
        {
            get => _filePathText;
            set
            {
                _filePathText = value;
               OnPropertyChanged("FilePathText");
            }
        }

        public ChartViewModel ChartViewModel { get; set; } = new ChartViewModel();

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
                FilePathText = openFileDialog.FileName;
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

                ChartViewModel.Data = process.Process();
            }
        }
    }
}
