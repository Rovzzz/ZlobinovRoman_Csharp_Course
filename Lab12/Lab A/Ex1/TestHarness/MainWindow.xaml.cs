using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using GaussianElimination;

namespace TestHarness
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdSolve_Click(object sender, RoutedEventArgs e)
        {
            double[,] coefficients = new double[Gauss.numberOfEquations, Gauss.numberOfEquations];

            double[] rhs = new double[Gauss.numberOfEquations];

            double[] answers = new double[Gauss.numberOfEquations];

            CollectAndValidateInput(coefficients, rhs);

            answers = Gauss.SolveGaussian(coefficients, rhs);
            DisplayResults(answers);
        }

        private void CollectAndValidateInput(double[,] coefficients, double[] rhs)
        {
            coefficients[0, 0] = double.Parse(this.w1.Text);
            coefficients[0, 1] = double.Parse(this.x1.Text);
            coefficients[0, 2] = double.Parse(this.y1.Text);
            coefficients[0, 3] = double.Parse(this.z1.Text);
            coefficients[1, 0] = double.Parse(this.w2.Text);
            coefficients[1, 1] = double.Parse(this.x2.Text);
            coefficients[1, 2] = double.Parse(this.y2.Text);
            coefficients[1, 3] = double.Parse(this.z2.Text);
            coefficients[2, 0] = double.Parse(this.w3.Text);
            coefficients[2, 1] = double.Parse(this.x3.Text);
            coefficients[2, 2] = double.Parse(this.y3.Text);
            coefficients[2, 3] = double.Parse(this.z3.Text);
            coefficients[3, 0] = double.Parse(this.w4.Text);
            coefficients[3, 1] = double.Parse(this.x4.Text);
            coefficients[3, 2] = double.Parse(this.y4.Text);
            coefficients[3, 3] = double.Parse(this.z4.Text);

            rhs[0] = double.Parse(this.r1.Text);
            rhs[1] = double.Parse(this.r2.Text);
            rhs[2] = double.Parse(this.r3.Text);
            rhs[3] = double.Parse(this.r4.Text);

            this.equation1.Content = string.Format("{0}w + {1}x + {2}y + {3}z = {4}", coefficients[0, 0], coefficients[0, 1], coefficients[0, 2], coefficients[0, 3], rhs[0]);
            this.equation2.Content = string.Format("{0}w + {1}x + {2}y + {3}z = {4}", coefficients[1, 0], coefficients[1, 1], coefficients[1, 2], coefficients[1, 3], rhs[1]);
            this.equation3.Content = string.Format("{0}w + {1}x + {2}y + {3}z = {4}", coefficients[2, 0], coefficients[2, 1], coefficients[2, 2], coefficients[2, 3], rhs[2]);
            this.equation4.Content = string.Format("{0}w + {1}x + {2}y + {3}z = {4}", coefficients[3, 0], coefficients[3, 1], coefficients[3, 2], coefficients[3, 3], rhs[3]);
        }

        private void DisplayResults(double[] answers)
        {
            this.results.Content = string.Format("w = {0}, x = {1}, y = {2}, z = {3}", answers[0], answers[1], answers[2], answers[3]);
        }        
    }
}
