﻿namespace FeedForwardNeuralNetwork
{
    public class NeuralNetwork
    {
        public Topology Topology { get; }
        public List<Layer> Layers { get; }

        public NeuralNetwork(Topology topology)
        {
            Topology = topology;

            Layers = new List<Layer>();

            CreateInputLayer();
            CreateHiddenLayers();
            CreateOutputLayer();
        }

        public Neuron Predict(params double[] inputSignals)
        {
            SendSignalsToInputNeurons(inputSignals);
            FeedForwardAllLayersAfterInput();

            if (Topology.OutputCount == 1)
            {
                return Layers.Last().Neurons[0];
            }
            else
            {
                return Layers.Last().Neurons.OrderByDescending(n => n.Output).First();
            }
        }

        public void Learn(double[] expected, double[,] inputs, int epoch)
        {
            for (int i = 0; i < epoch; i++)
            {
                for (int j = 0; j < expected.Length; j++)
                {
                    var output = expected[j];
                    var input = GetRow(inputs, j);

                    Backpropagation(output, input);
                }
            }
        }

        public static double[] GetRow(double[,] matrix, int row)
        {
            var columns = matrix.GetLength(1);
            var array = new double[columns];
            for (int i = 0; i < columns; ++i)
                array[i] = matrix[row, i];
            return array;
        }


        private void Backpropagation(double exprected, params double[] inputs)
        {
            var actual = Predict(inputs).Output;

            var error = actual - exprected;

            foreach (var neuron in Layers.Last().Neurons)
            {
                neuron.Learn(error, Topology.LearningRate);
            }

            for (int j = Layers.Count - 2; j >= 0; j--)
            {
                var layer = Layers[j];
                var previousLayer = Layers[j + 1];

                for (int i = 0; i < layer.NeuronCount; i++)
                {
                    var neuron = layer.Neurons[i];
                    
                    double previousDeltaMultiplySpecificWeights = 0.0;
                    
                    for (int k = 0; k < previousLayer.NeuronCount; k++)
                    {
                        var previousNeuron = previousLayer.Neurons[k];

                        previousDeltaMultiplySpecificWeights += previousNeuron.Weights[i] * previousNeuron.Delta;
                        
                    }
                    neuron.Learn(error, Topology.LearningRate, previousDeltaMultiplySpecificWeights);
                }
            }
            
        }

        private void FeedForwardAllLayersAfterInput()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                var previousLayerSingals = Layers[i - 1].GetSignals();

                foreach (var neuron in layer.Neurons)
                {
                    neuron.FeedForward(previousLayerSingals);
                }
            }
        }

        private void SendSignalsToInputNeurons(params double[] inputSignals)
        {
            for (int i = 0; i < inputSignals.Length; i++)
            {
                var signal = new List<double>() { inputSignals[i] };
                var neuron = Layers[0].Neurons[i];

                neuron.FeedForward(signal);
            }
        }

        private void CreateOutputLayer()
        {
            var outputNeurons = new List<Neuron>();
            var lastLayer = Layers.Last();
            for (int i = 0; i < Topology.OutputCount; i++)
            {
                var neuron = new Neuron(lastLayer.NeuronCount, NeuronType.Output);
                outputNeurons.Add(neuron);
            }
            var outputLayer = new Layer(outputNeurons, NeuronType.Output);
            Layers.Add(outputLayer);
        }

        private void CreateHiddenLayers()
        {
            for (int j = 0; j < Topology.HiddenLayers.Count; j++)
            {
                var hiddenNeurons = new List<Neuron>();
                var lastLayer = Layers.Last();
                for (int i = 0; i < Topology.HiddenLayers[j]; i++)
                {
                    var neuron = new Neuron(lastLayer.NeuronCount);
                    hiddenNeurons.Add(neuron);
                }
                var hiddenLayer = new Layer(hiddenNeurons);
                Layers.Add(hiddenLayer);
            }
        }

        private void CreateInputLayer()
        {
            var inputNeurons = new List<Neuron>();
            for (int i = 0; i < Topology.InputCount; i++)
            {
                var neuron = new Neuron(1, NeuronType.Input);
                inputNeurons.Add(neuron);
            }
            var inputLayer = new Layer(inputNeurons, NeuronType.Input);
            Layers.Add(inputLayer);
        }
    }
}
