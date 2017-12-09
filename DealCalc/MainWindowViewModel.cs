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
    internal class MainWindowViewModel : ViewModel
    {
        private string _filePathText;
        private string _title;
        public string FilePathText
        {
            get => _filePathText;
            set
            {
                _filePathText = value;
               OnPropertyChanged("FilePathText");
            }
        }

        public String Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private ChartViewModel _chartViewModel;
        private ChartViewModel.Type _chartType = ChartViewModel.Type.Normal;

        public ChartViewModel.Type ChartType
        {
            get => _chartType;
            set
            {
                _chartType = value;

                if (ChartViewModel != null)
                {
                    ChartViewModel.ChartType = _chartType;
                }

                OnPropertyChanged("ChartType");
            }
        } 

        public ChartViewModel ChartViewModel
        {
            get => _chartViewModel;
            set
            {
                _chartViewModel = value;
                OnPropertyChanged("ChartViewModel");
            }
        }

        public ICommand OpenFileCommand => new DelegateCommand(OpenFile);
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


                //600020 中原高速 1分钟线 前复权
                string titleLine = text[0];

                string[] parts = titleLine.Split(null);

                Title = parts[0] + " " + parts[1];

                var list = text.Select(TransactionData.CreateFromString).Where(data => data != null).ToList();

                var process = new CoreProcessor(list);

                ChartViewModel = new ChartViewModel(process.Process(), ChartType);
            }
        }
    }
}
