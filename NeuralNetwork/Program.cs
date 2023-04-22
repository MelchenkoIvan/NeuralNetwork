namespace FeedForwardNeuralNetwork
{
    internal class Program
    {
        public static void IsSick(NeuralNetwork neuralNetwork)
        {
            Console.WriteLine("Enter the answers YES/NO");
            Console.WriteLine("If line is empty will be recognized as NO");

            var stopPrediction = false;
            while (!stopPrediction)
            {
                Console.WriteLine("Is the temperature elevated?");
                var temAnswer = Console.ReadLine() ?? "NO";
                var temp = (int)Enum.Parse(typeof(YesNo), temAnswer, true);

                Console.WriteLine("Are you over 30 years old?");
                var yearsAnswer = Console.ReadLine() ?? "NO";
                var years = (int)Enum.Parse(typeof(YesNo), temAnswer, true);

                Console.WriteLine("Are you smoke?");
                var smokeAnswer = Console.ReadLine() ?? "NO";
                var smoke = (int)Enum.Parse(typeof(YesNo), temAnswer, true);

                Console.WriteLine("Are you eating right?");
                var eatingAnswer = Console.ReadLine() ?? "NO";
                var eating = (int)Enum.Parse(typeof(YesNo), temAnswer, true);

                int res = (int)Math.Round(neuralNetwork.Predict(new double[] { temp, years, smoke, eating }).Output);

                if (res == 1)
                    Console.WriteLine("You are sick");
                if (res == 0)
                    Console.WriteLine("You are healthy");

                Console.WriteLine("Do you want predict a new health result? ");
                var stopPredictionAnswer = Console.ReadLine() ?? "NO";
                stopPrediction = (int)Enum.Parse(typeof(YesNo), temAnswer, true) == 1 ? true : false;
            }
        }

        public static NeuralNetwork LearnSickModel()
        {
            var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0 };
            //var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };

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
                { 1, 1, 0, 1 } //,
                //{ 1, 1, 1, 0 },
                //{ 1, 1, 1, 1 }
            };

            var topology = new Topology(4, 1, 0.1, 3, 2);
            var neuralNetwork = new NeuralNetwork(topology);
            Console.WriteLine("We are learning health NN now");
            var startTime = DateTime.Now;
            neuralNetwork.Learn(outputs, inputs, 100000);
            Console.WriteLine($"NN learnd during: {DateTime.Now - startTime}");

            return neuralNetwork;
        }

        public static void HeartIsSick(NeuralNetwork neuralNetwork)
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

            var results = new List<double>();
            var inputSignals = new double[inputs.Count, inputs[0].Length];
            for (int i = 0; i < inputSignals.GetLength(0); i++)
            {
                for (int j = 0; j < inputSignals.GetLength(1); j++)
                {
                    inputSignals[i, j] = inputs[i][j];
                }
            }

            var normalizedInputs = DataSetHelper.Scalling(inputSignals);
            var normalizedInputsSignalsList = normalizedInputs.ToListArrays();

            var correct = 0;
            for (int i = 0; i < outputs.Count; i++)
            {
                var row = normalizedInputsSignalsList[i];
                var res = (int)Math.Round(neuralNetwork.Predict(row).Output);
                if (outputs[i] == res)
                {
                    correct += 1;
                    Console.WriteLine("Result is equal target");
                }
                else
                    Console.WriteLine("Result is not equal target");
            }

            var predictionAccuracy = (correct * 100) / outputs.Count;
            Console.WriteLine($"Prediction accurcy - {predictionAccuracy}%");
        }

        static void Main(string[] args)
        {
            var heartSickNNModel = Executor.LearnHeartSickModel();
            HeartIsSick(heartSickNNModel);
        }
    }
}