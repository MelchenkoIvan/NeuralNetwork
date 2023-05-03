using FeedForwardNeuralNetwork;
using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Endpoints.Authorization;
using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.NeuralNetwork;

[Route("[controller]")]
[ApiController]
[Authorize]
public class NeuralNetworkController : Controller
{
    [HttpPost("feedforward")]
    public async Task<double> FeedFroward(SymptomesDTO req)
    {
        var result = Executor.Predict(req.GetInputSignals());
        return await Task.FromResult(result);
    }
    
    [HttpPost("recurrent")]
    public async Task<double> Recurrent(SymptomesDTO req)
    {
        return await Task.FromResult(1);
    }
}