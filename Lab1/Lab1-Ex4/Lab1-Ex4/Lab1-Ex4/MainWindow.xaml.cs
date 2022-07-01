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

namespace Lab1_Ex4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor for Main Window
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read a line of data entered by the user.
        /// Format the data and display the results in the
        /// formattedText TextBlock control
        /// </summary>
        /// <param name = "sender" ></param>
        /// <param name = "e" ></param>

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            string line = testInput.Text;
            line = line.Replace(",", " y:");
            line = "x:" + line;
            formattedText.Text = line;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Buffer to hold a line read from the file on standard input

            StreamReader sr = new StreamReader("DataFile.txt");

            // Loop until the end of the file
            while (!sr.EndOfStream)
            {
                //Reading the file completely
                string line = sr.ReadToEnd();
                // Format the data in the buffer
                line = line.Replace(",", " y:");
                line = line.Replace("*", "x:");
                // Put the results into the TextBlock
                formattedText.Text = line;
            }
            sr.Close();
        }
    }
}
