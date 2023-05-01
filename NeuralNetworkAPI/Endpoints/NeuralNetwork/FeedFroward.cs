using FeedForwardNeuralNetwork;
using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.NeuralNetwork;
[HttpPost("neuralNetwork/feedforward")]
public class FeedFroward : Endpoint<SymptomesDTO, double>
{
    public override Task<double> ExecuteAsync(SymptomesDTO req, CancellationToken ct)
    {
        var result = Executor.Predict(req.GetInputSignals());
        return Task.FromResult(result);
    }
}