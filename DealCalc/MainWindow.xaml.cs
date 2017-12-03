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

            foreach (string s in text)
            {
                textPath.Content = s;
            }

            Debug.WriteLine(TransactionData.CreateFromString(rawData: line1)); 

            var list = text.Select(TransactionData.CreateFromString).Where(data => data != null).ToList();

            var process = new CoreProcessor(list);
            process.Process();
        }

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

                foreach (string s in text)
                {
                    textPath.Content = s;
                }

            }

        }
    }
}
