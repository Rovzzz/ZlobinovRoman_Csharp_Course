using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixMultiplication
{
    /// <summary>
    /// Static class to perform a variety of matrix manipulations
    /// </summary>
    static class Matrix
    {
        public static double[,] MatrixMultiply(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(1)) 
                throw new ArgumentException("Количество столбцов в первой матрице должно совпадать с количеством строк во второй матрице");

            int m1columns_m2rows = matrix1.GetLength(0);
            int m1rows = matrix1.GetLength(1);
            int m2columns = matrix2.GetLength(0);

            double[,] result = new double[m2columns, m1rows];

            for (int row = 0; row < m1rows; row++)
            {
                for (int column = 0; column < m2columns; column++)
                {
                    double accumulator = 0;
                    
                    for (int cell = 0; cell < m1columns_m2rows; cell++)
                    {
                        if (matrix1[cell, row] < 0d) 
                            throw new ArgumentException(
                                string.Format("Матрица 1 содержит недопустимую запись в ячейке {0},{1}", cell, row));

                        if (matrix2[column, cell] < 0d)
                            throw new ArgumentException(
                                string.Format("Матрица 2 содержит недопустимую запись в ячейке {0},{1}",column, cell));

                        accumulator += matrix1[cell, row] * matrix2[column, cell];
                    }
                    result[column, row] = accumulator;
                }
            }
            return result;
        }
    }
}
