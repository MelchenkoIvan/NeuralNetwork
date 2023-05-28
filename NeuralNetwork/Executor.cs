namespace FeedForwardNeuralNetwork
{
    public static class Executor
    {
        internal static NeuralNetwork LearnHeartSickModel()
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

            Console.WriteLine("We are learning heart health NN now");
            var startTime = DateTime.Now;

            var hiddenLayersCountCount = inputLayerNeuronsCount - 2;

            var hiddenLayers = new int[hiddenLayersCountCount];
            for (var i = 0; i < hiddenLayersCountCount; i++)
            {
                hiddenLayers[i] = inputLayerNeuronsCount - 1 - i;
            }

            var topology = new Topology(inputLayerNeuronsCount, 1, 0.1, 6);
            var neuralNetwork = new NeuralNetwork(topology);

            var normalizedInputs = DataSetHelper.Scalling(inputSignals);

            neuralNetwork.Learn(outputs.ToArray(), normalizedInputs, 50000);
            Console.WriteLine($"NN learnd during: {DateTime.Now - startTime}");
            return neuralNetwork;
        }

        public static double Predict(double[,] inputSignals) =>
            LearnHeartSickModel()
                .Predict(DataSetHelper.Scalling(inputSignals).ToListArrays().Single())
                .Output;
    }
}