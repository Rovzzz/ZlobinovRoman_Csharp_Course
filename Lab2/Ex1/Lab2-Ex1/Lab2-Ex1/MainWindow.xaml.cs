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

namespace Lab2_Ex1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            string input = inputTextBlock.Text;
            bool check1 = double.TryParse(input, out double numberDouble);
            if (check1 == true)
            {
               string result1 = Convert.ToString(Math.Sqrt(numberDouble));
               frameworkLabel.Content = $"Using .NET Framework = {result1}";
            }
            else
            {
                MessageBox.Show("Пожалуйста введите double");
                return;
            }

            decimal numberDecimal = Convert.ToDecimal(inputTextBlock.Text);
            decimal delta = Convert.ToDecimal(Math.Pow(10, -28));
            decimal guess = numberDecimal/2;
            bool check2 = decimal.TryParse(input, out numberDecimal);
            if(check2 == true)
            {
                decimal result2 = ((numberDecimal / guess) + guess) / 2;

                while (Math.Abs(result2 - guess) > delta)
                {
                    guess = result2;
                    result2 = ((numberDecimal / guess) + guess) / 2;
                    newtonLabel.Content = Convert.ToString($"Using Newton = {result2}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста введите decimal");
                return;
            }

        }
    }
}
