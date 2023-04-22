using FeedForwardNeuralNetwork;
using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.NeuralNetwork;
[HttpPost("neuralNetwork/feedforward")]
public class FeedFroward : Endpoint<SymptomsDTO>
{
    public override Task HandleAsync(SymptomsDTO req, CancellationToken ct)
    {
        var result = Executor.PredictForClient(req.GetInputSignals());
        return Task.CompletedTask;
    }
}