using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.NeuralNetwork;
[HttpPost("neuralNetwork/recurrent")]
public class Recurrent : Endpoint<SymptomesDTO, double>
{
    public override async Task<double> ExecuteAsync(SymptomesDTO req, CancellationToken ct)
    {
        return await Task.FromResult(1);
    }
}