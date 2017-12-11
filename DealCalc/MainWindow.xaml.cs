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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace DealCalc
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ((ViewModel) DataContext).ShowErrorMessageRequest += ShowErrorMessage;
        }

        private void ShowErrorMessage(object sender, String errorMessage)
        {
            this.ShowMessageAsync("错误", errorMessage, MessageDialogStyle.Affirmative, new MetroDialogSettings { AffirmativeButtonText = "好的" });
        }
    }
}
