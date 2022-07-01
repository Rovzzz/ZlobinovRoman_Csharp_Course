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

namespace Lab3_Ex1
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

        private void findGCD_Click(object sender, RoutedEventArgs e)
        {
            int firstNumber;
            int secondNumber;
            int thirdNumber;
            int fourthNumber;
            int fifthNumber;

            if (!GetPostiveIntegerFromTextBox(integer1, out firstNumber)) return;
            if (!GetPostiveIntegerFromTextBox(integer2, out secondNumber)) return;
            if (!GetPostiveIntegerFromTextBox(integer3, out thirdNumber)) return;
            if (!GetPostiveIntegerFromTextBox(integer4, out fourthNumber)) return;
            if (!GetPostiveIntegerFromTextBox(integer5, out fifthNumber)) return;


            if (sender == findGCD) 
            {
                this.resultEuclid.Content =
                String.Format("Euclid: {0}",
                GCDAlgorithms.FindGCDEuclid(firstNumber, secondNumber));
            }

        }
        private bool GetPostiveIntegerFromTextBox(TextBox textBox, out int i)
        {
        i = -1;
        if (int.TryParse(textBox.Text, out i))
        {
            if (i >= 0) return true;
        }
        MessageBox.Show("Не положительное число: " + textBox.Text);
        return false;
        }
    }

}

