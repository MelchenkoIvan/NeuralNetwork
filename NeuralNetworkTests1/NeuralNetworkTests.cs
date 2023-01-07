using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetwork.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
            var inputs = new double[,]
            {
                // Результат - Пациент болен - 1
                //             Пациент Здоров - 0

                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
                //T  A  S  F
                { 0, 0, 0, 0 },
                { 0, 0, 0, 1 },
                { 0, 0, 1, 0 },
                { 0, 0, 1, 1 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 1 },
                { 0, 1, 1, 0 },
                { 0, 1, 1, 1 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 1 },
                { 1, 0, 1, 0 },
                { 1, 0, 1, 1 },
                { 1, 1, 0, 0 },
                { 1, 1, 0, 1 },
                { 1, 1, 1, 0 },
                { 1, 1, 1, 1 }
            };

            var topology = new Topology(4, 1, 0.1, 3, 2);
            var neuralNetwork = new NeuralNetwork(topology);
            var difference = neuralNetwork.Learn(outputs, inputs, 10000);

            var results = new List<double>();
            for (int i = 0; i < outputs.Length; i++)
            {
                var row = NeuralNetwork.GetRow(inputs, i);
                var res = neuralNetwork.Predict(row).Output;
                results.Add(res);
            }

            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 1);
                var actual = Math.Round(results[i], 1);
                Assert.AreEqual(expected, actual);
            }
        }
        [TestMethod()]
        public void DataSetTest()
        {
            var outputs = new List<double>();
            var inputs = new List<double[]>();
            int inputLayerNuronsCount = 0;

            using (var sr = new StreamReader("heart.csv"))
            {
                var header = sr.ReadLine();
                inputLayerNuronsCount = header!.Split(',').Count();
                while (!sr.EndOfStream)
                {
                    var row = sr.ReadLine();
                    var values = row.Split(',').Select(v => Convert.ToDouble(v.Replace('.', ','))).ToList();
                    var output = values.Last();
                    var input = values.Take(values.Count - 1).ToArray();

                    outputs.Add(output);
                    inputs.Add(input);
                }
            }
            var inputSignals = new double[inputs.Count,inputs[0].Length];

            for (int i = 0; i < inputSignals.GetLength(0); i++)
            {
                for (int j = 0; j < inputSignals.GetLength(1); j++)
                {
                    inputSignals[i, j] = inputs[i][j];
                }
            }
            var hiddenLeyersCountCount = inputLayerNuronsCount - 2;

            int[] hiddenLayers = new int[hiddenLeyersCountCount];
            for (int i = 0; i < hiddenLeyersCountCount; i++)
            {
                hiddenLayers[i] = inputLayerNuronsCount - 1 - i;
            }

            var topology = new Topology(inputLayerNuronsCount, 1, 0.1, hiddenLayers);
            var neuralNetwork = new NeuralNetwork(topology);

            var normalizedInputs =  DataSetHelper.Normalization(inputSignals);

            var difference = neuralNetwork.Learn(outputs.ToArray(), normalizedInputs, 10000);

            var results = new List<double>();
            var normalizedInputsSignalsList = normalizedInputs.ToListArrays();

            for (int i = 0; i < outputs.Count; i++)
            {
                var row = normalizedInputsSignalsList[i];
                var res = neuralNetwork.Predict(normalizedInputsSignalsList[i]).Output;
                results.Add(res);
            }

            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 1);
                var actual = Math.Round(results[i], 1);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}