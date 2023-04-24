using FeedForwardNeuralNetwork;
using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.NeuralNetwork;
[HttpPost("neuralNetwork/feedforward")]
public class FeedFroward : Endpoint<SymptomesDTO>
{
    public override Task HandleAsync(SymptomesDTO req, CancellationToken ct)
    {
        var result = Executor.PredictForClient(req.GetInputSignals());
        return Task.CompletedTask;
    }
}