namespace RecurrentNeuralNetwork
{
    internal class Program
    {
        public static void IsSick(NeuralNetwork neuralNetwork)
        {
            neuralNetwork.RecurrentPredict(new List<Tact>()
            {
                new Tact(1,new double[] { 1, 1, 1, 1 }),
                new Tact(2,new double[] { 1, 1, 0, 1 }),
                new Tact(3,new double[] { 1, 1, 1, 0 }),
                new Tact(4,new double[] { 1, 0, 1, 1 }),
            });
            
            var res = (int)Math.Round(neuralNetwork.Predict(new double[] { 1, 1, 1, 1 }).Output);
            Console.WriteLine(res);
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

            var topology = new Topology(4, 1, 0.1, 2);
            var neuralNetwork = new NeuralNetwork(topology);
            Console.WriteLine("We are learning health NN now");
            var startTime = DateTime.Now;
            //neuralNetwork.Learn(outputs, inputs, 100000);
            Console.WriteLine($"NN learnd during: {DateTime.Now - startTime}");

            return neuralNetwork;
        }
        static void Main(string[] args)
        {
            var sickModel = LearnSickModel();
            IsSick(sickModel);
        }
    }
}