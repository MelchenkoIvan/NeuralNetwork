using System;
namespace RecurrentNeuralNetwork
{
	public class NeuralNetwork
	{
        public Topology Topology { get; }
        public List<Layer> Layers { get; }

        public NeuralNetwork(Topology topology)
        {
            Topology = topology;

            Layers = new List<Layer>();

            //CreateInputLayer();
            //CreateHiddenLayers();
            //CreateOutputLayer();
        }
    }
}

