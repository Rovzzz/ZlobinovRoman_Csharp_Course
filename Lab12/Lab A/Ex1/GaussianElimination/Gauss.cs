using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GaussianElimination
{
    public static class Gauss
    {
        static Hashtable results;

        public const int numberOfEquations = 4;

        public static double[] SolveGaussian(double[,] coefficients, double[] rhs)
        {
            if (results == null)
            {
                results = new Hashtable();
            }

            StringBuilder hashString = new StringBuilder();
            foreach (double coefficient in coefficients)
            {
                hashString.Append(coefficient);
            }
            foreach (double value in rhs)
            {
                hashString.Append(value);
            }
            string hashValue = hashString.ToString();

            if (results.Contains(hashValue))
            {
                return (double[])results[hashValue];
            }
            else
            {

                double[,] coefficientsCopy = DeepCopy2D(coefficients);
                double[] rhsCopy = DeepCopy1D(rhs);

                double x, sum;

                for (int k = 0; k < numberOfEquations - 1; k++)
                {
                    try
                    {
                        for (int i = k + 1; i < numberOfEquations; i++)
                        {
                            x = coefficientsCopy[i, k] / coefficientsCopy[k, k];
                            for (int j = k + 1; j < numberOfEquations; j++)
                                coefficientsCopy[i, j] = coefficientsCopy[i, j] - coefficientsCopy[k, j] * x;

                            rhsCopy[i] = rhsCopy[i] - rhsCopy[k] * x;
                        }
                    }
                    catch (DivideByZeroException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                rhsCopy[numberOfEquations - 1] = rhsCopy[numberOfEquations - 1] / coefficientsCopy[numberOfEquations - 1, numberOfEquations - 1];
                for (int i = numberOfEquations - 2; i >= 0; i--)
                {
                    sum = rhsCopy[i];
                    for (int j = i + 1; j < numberOfEquations; j++)
                        sum = sum - coefficientsCopy[i, j] * rhsCopy[j];
                    rhsCopy[i] = sum / coefficientsCopy[i, i];
                }
                System.Threading.Thread.Sleep(5000);

                results.Add(hashValue, rhsCopy);
                return rhsCopy;
            }
        }

        private static double[] DeepCopy1D(double[] array)
        {
            int columns = array.GetLength(0);

            double[] newArray = new double[columns];

            for (int i = 0; i < columns; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }

        private static double[,] DeepCopy2D(double[,] array)
        {
            int columns = array.GetLength(0);
            int rows = array.GetLength(1);

            double[,] newArray = new double[columns, rows];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    newArray[i, j] = array[i, j];
                }
            }
            return newArray;
        }
    }
}
