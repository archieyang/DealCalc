﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
               OnPropertyChanged();
            }
        }

        public String Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
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
            try
            {
                if ((bool) openFileDialog.ShowDialog())
                {
                    FilePathText = openFileDialog.FileName;
                    string[] text = System.IO.File.ReadAllLines(openFileDialog.FileName, Encoding.Default);

                    string titleLine = text[0];

                    string[] parts = titleLine.Split(null);

                    if (parts.Length == 4 && parts[0].Length == 6)
                    {
                        Title = parts[0] + " " + parts[1];
                    }
                    else
                    {
                        AlertError("导入表单标题栏错误");
                    }

                    string columnNameLine = text[1];

                    string[] columeNames = columnNameLine.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);


                    var columnNameDict = new Dictionary<String, int>();
                    if (columeNames.Length == 8)
                    {
                        for (var index = 0; index < columeNames.Length; index++)
                        {
                            columnNameDict.Add(columeNames[index].Trim(), index);
                        }
                    }
                    else
                    {
                        AlertError("导入的表单包含的列数量错误");
                    }

                    if (columnNameDict.Count != 8) return;

                    var list = text.Skip(2)
                        .Select(textLine => TransactionData.CreateFromString(textLine, columnNameDict, AlertError))
                        .Where(data => data != null).ToList();

                    if (list.Count == 0)
                    {
                        AlertError(@"导入表单失败，请使用空格作为分隔符，日期样式为YYYY/MM/DD");
                    }
                    else
                    {
                        var process = new CoreProcessor(list) {ErrorHandler = AlertError};
                        ChartViewModel.Data = process.Process();
                    }
                }

            }
            catch (Exception e)
            {
                AlertError("打开表单时发生异常：" + e);
            }
        }
    }
}
