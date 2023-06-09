using NUnit.Framework;
using RecurrentNeuralNetwork;
using DataSetHelper = FeedForwardNeuralNetwork.DataSetHelper;

namespace NeuralNetworksTests;

public class NeuralNetworksEffectivenessTests
{
    [Test]
    public void FeedForwardSpeed()
    {
        var learnData = GetLearnData();
        var topology = new FeedForwardNeuralNetwork.Topology(learnData.InputLayerNeuronsCount, 1, 0.01, 6);
        var neuralNetwork = new FeedForwardNeuralNetwork.NeuralNetwork(topology);
        neuralNetwork.Learn(learnData.Outputs, learnData.NormalizedInputs, 3000);
        var testData = GetTestData();

        var times = new List<TimeSpan>();
        for (int i = 0; i < testData.Outputs.Count; i++)
        {
            var row = testData.NormalizedInputs[i];
            var startTime = DateTime.Now;
            var res = (int)Math.Round(neuralNetwork.Predict(row).Output);
            times.Add(DateTime.Now - startTime);
        }
    }

    [Test]
    public void RecurrentSpeed()
    {
        var learnData = GetLearnData();
        var topology = new RecurrentNeuralNetwork.Topology(learnData.InputLayerNeuronsCount, 1, 0.01, 6);
        var neuralNetwork = new RecurrentNeuralNetwork.NeuralNetwork(topology);
        neuralNetwork.Learn(learnData.Outputs, learnData.NormalizedInputs, 50000);

        var testData = GetTestData();

        var times = new List<TimeSpan>();
        for (int i = 0; i < testData.Outputs.Count; i++)
        {
            var row = testData.NormalizedInputs[i];
            var tacts = new List<Tact>();
            for (int j = 1; j < 4; j++)
            {
                tacts.Add(new Tact(j, row));
            }

            var startTime = DateTime.Now;
            var res = (int)Math.Round(neuralNetwork.RecurrentPredict(tacts).Output);
            times.Add(DateTime.Now - startTime);
        }
    }

    [Test]
    public void FeedForwardAccuracy()
    {
        var learnData = GetLearnData();
        var topology = new FeedForwardNeuralNetwork.Topology(learnData.InputLayerNeuronsCount, 1, 0.01, 6);
        var neuralNetwork = new FeedForwardNeuralNetwork.NeuralNetwork(topology);
        neuralNetwork.Learn(learnData.Outputs, learnData.NormalizedInputs, 3000);
        var testData = GetTestData();

        var predictionsAccuracy = new List<string>();
        var correct = 0;
        for (int i = 0; i < testData.Outputs.Count; i++)
        {
            var row = testData.NormalizedInputs[i];
            var res = (int)Math.Round(neuralNetwork.Predict(row).Output);
            if (testData.Outputs[i] == res)
            {
                predictionsAccuracy.Add("100%");
                correct += 1;
            }
            else
            {
                predictionsAccuracy.Add("0%");
            }
        }
        var predictionAccuracy = (correct * 100) / testData.Outputs.Count;
    }

    [Test]
    public void RecurrentAccuracy()
    {
        var learnData = GetLearnData();
        var topology = new RecurrentNeuralNetwork.Topology(learnData.InputLayerNeuronsCount, 1, 0.01, 6);
        var neuralNetwork = new RecurrentNeuralNetwork.NeuralNetwork(topology);
        neuralNetwork.Learn(learnData.Outputs, learnData.NormalizedInputs, 3000);
        var testData = GetTestData();

        var predictionsAccuracy = new List<string>();

        var correct = 0;
        for (int i = 0; i < testData.Outputs.Count; i++)
        {
            var row = testData.NormalizedInputs[i];
            var tacts = new List<Tact>();
            for (int j = 1; j < 4; j++)
            {
                tacts.Add(new Tact(j, row));
            }

            var res = (int)Math.Round(neuralNetwork.RecurrentPredict(tacts).Output);
            if (testData.Outputs[i] == res)
            {
                predictionsAccuracy.Add("100%");
                correct += 1;
            }
            else
            {
                predictionsAccuracy.Add("0%");
            }
        }

        var predictionAccuracy = (correct * 100) / testData.Outputs.Count;
    }

    private TestData GetTestData()
    {
        var outputs = new List<double>();
        var inputs = new List<double[]>();
        int inputLayerNuronsCount = 0;
        using (var sr = new StreamReader(
                   "/Users/ivanmelchenko/Repos/NeuralNetwork/NeuralNetworkTests1/heartTest.csv"))
        {
            var header = sr.ReadLine();
            inputLayerNuronsCount = header!.Split(',').Count() - 1;
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

        var inputSignals = new double[inputs.Count, inputs[0].Length];
        for (int i = 0; i < inputSignals.GetLength(0); i++)
        {
            for (int j = 0; j < inputSignals.GetLength(1); j++)
            {
                inputSignals[i, j] = inputs[i][j];
            }
        }

        var normalizedInputs = FeedForwardNeuralNetwork.DataSetHelper.Scalling(inputSignals);
        var normalizedInputsSignalsList = DataSetHelper.ToListArrays(normalizedInputs);
        return new TestData(normalizedInputsSignalsList, outputs);
    }

    private class TestData
    {
        public TestData(List<double[]> normalizedInputs, List<double> outputs)
        {
            NormalizedInputs = normalizedInputs;
            Outputs = outputs;
        }

        public List<double[]> NormalizedInputs { get; }

        public List<double> Outputs { get; }
    }

    private LearnData GetLearnData()
    {
        var outputs = new List<double>();
        var inputs = new List<double[]>();
        var inputLayerNeuronsCount = 0;

        using (var sr = new StreamReader("/Users/ivanmelchenko/Repos/NeuralNetwork/NeuralNetworkTests1/heart.csv"))
        {
            var header = sr.ReadLine();

            inputLayerNeuronsCount = header!.Split(',').Count() - 1;
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

        var inputSignals = new double[inputs.Count, inputs[0].Length];

        for (int i = 0; i < inputSignals.GetLength(0); i++)
        {
            for (int j = 0; j < inputSignals.GetLength(1); j++)
            {
                inputSignals[i, j] = inputs[i][j];
            }
        }

        var hiddenLayersCountCount = inputLayerNeuronsCount - 2;

        var hiddenLayers = new int[hiddenLayersCountCount];
        for (var i = 0; i < hiddenLayersCountCount; i++)
        {
            hiddenLayers[i] = inputLayerNeuronsCount - 1 - i;
        }

        var normalizedInputs = FeedForwardNeuralNetwork.DataSetHelper.Scalling(inputSignals);
        return new LearnData(normalizedInputs, outputs.ToArray(), inputLayerNeuronsCount);
    }

    private class LearnData
    {
        public LearnData(double[,] normalizedInputs, double[] outputs, int inputLayerNeuronsCount)
        {
            NormalizedInputs = normalizedInputs;
            Outputs = outputs;
            InputLayerNeuronsCount = inputLayerNeuronsCount;
        }

        public double[,] NormalizedInputs { get; }

        public double[] Outputs { get; }

        public int InputLayerNeuronsCount { get; }
    }
}