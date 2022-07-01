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
using System.IO;

namespace Lab1_Ex2_WPF_Application
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            string line = testInput.Text;
            line = line.Replace(",", " y:");
            line = "x:" + line;
            formattedText.Text = line;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader("DataFile.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadToEnd();
                line = line.Replace(",", " y:");
                line = line.Replace("*", "x:");
                formattedText.Text = line;
            }
            sr.Close();
        }
    }
}