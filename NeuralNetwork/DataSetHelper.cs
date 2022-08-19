using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public static class DataSetHelper
    {
        public static List<double[]> ToListArrays(this double[,] dataSet)
        {
            var result = new List<double[]>();

            var itemsCount = dataSet.GetLength(0);
            var itemLength = dataSet.GetLength(1);
            for (int i = 0; i < itemsCount; i++)
            {
                var item = new double[itemLength];
                for (int j = 0; j < itemLength; j++)
                {
                    item[j] = dataSet[i, j];
                }
                result.Add(item);
            }

            return result;
        }

        public static double[,] Scalling(double[,] inputs)
        {
            var result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int column = 0; column < inputs.GetLength(1); column++)
            {
                var min = inputs[0, column];
                var max = inputs[0, column];

                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    var item = inputs[row, column];

                    if (item < min)
                    {
                        min = item;
                    }

                    if (item > max)
                    {
                        max = item;
                    }
                }

                var divider = max - min;
                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    result[row, column] = (inputs[row, column] - min) / divider;
                }
            }

            return result;
        }

        public static double[,] Normalization(double[,] inputs)
        {
            var result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int column = 0; column < inputs.GetLength(1); column++)
            {
                // Среднее значение сигнала нейрона.
                var sum = 0.0;
                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    sum += inputs[row, column];
                }
                var average = sum / inputs.GetLength(0);

                // Стандартное квадратичное отклонение нейрона.
                var error = 0.0;
                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    error += Math.Pow((inputs[row, column] - average), 2);
                }
                var standardError = Math.Sqrt(error / inputs.GetLength(0));

                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    result[row, column] = (inputs[row, column] - average) / standardError;
                }
            }

            return result;
        }
    }
}
