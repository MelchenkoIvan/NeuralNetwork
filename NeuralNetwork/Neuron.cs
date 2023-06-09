namespace FeedForwardNeuralNetwork
{
    public class Neuron
    {
        public List<double> Weights { get; }

        public List<double> Inputs { get; }
        
        public NeuronType NeuronType { get; }
        
        public double Output { get; private set; }
        
        public double Delta { get; private set; }

        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            NeuronType = type;
            Weights = new List<double>();
            Inputs = new List<double>();

            InitWeightsRandomValue(inputCount);
        }

        private void InitWeightsRandomValue(int inputCount)
        {
            var rnd = new Random();

            for (int i = 0; i < inputCount; i++)
            {
                if (NeuronType == NeuronType.Input)
                {
                    Weights.Add(1);
                }
                else
                {
                    Weights.Add(rnd.NextDouble());
                }

                Inputs.Add(0);
            }
        }

        public double FeedForward(List<double> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                Inputs[i] = inputs[i];
            }

            var sum = 0.0;
            for (int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            if (NeuronType != NeuronType.Input)
            {
                Output = Sigmoid(sum);
            }
            else
            {
                Output = sum;
            }

            return Output;
        }

        private double Sigmoid(double x)
        {
            var result = 1.0 / (1.0 + Math.Pow(Math.E, -x));
            return result;
        }
        
        private double DerivativeSigmoid(double x)
        {
            var result = x * (1 - x);
            return result;
        }
        
        public void Learn(double error, double learningRate, double? previousDeltaMultiplySpecificWeights = null)
        {
            if (NeuronType == NeuronType.Input)
            {
                return;
            }

            if (NeuronType == NeuronType.Output)
            {
                Delta = error * DerivativeSigmoid(Output);
            }

            if (NeuronType == NeuronType.Normal || NeuronType == NeuronType.Input)
            {
                if (previousDeltaMultiplySpecificWeights == null)
                    throw new Exception("previousDeltaMultiplySpecificWeights can not " +
                                        "be calculated in this neuron because it is important " +
                                        "to know previous neuron relationship weights and delta.");
                
                Delta = (double)previousDeltaMultiplySpecificWeights * DerivativeSigmoid(Output);
            }

            for (int i = 0; i < Weights.Count; i++)
            {
                
                var weight = Weights[i];
                var input = Inputs[i];

                var newWeigth = weight - input * Delta * learningRate;
                
                Weights[i] = newWeigth;
            }
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}