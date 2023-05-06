using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Endpoints.Authorization;
using NeuralNetworkCore;
using NeuralNetworkCore.Models;

namespace NeuralNetworkAPI.Endpoints.NeuralNetwork;

[Route("[controller]")]
[ApiController]
[Authorize]
public class NeuralNetworkController : Controller
{
    private readonly INeuralNetworkRepository _neuralNetworkRepository;
    private readonly IUserRepositroy _userRepositroy;
    public NeuralNetworkController(INeuralNetworkRepository neuralNetworkRepository, IUserRepositroy userRepositroy)
    {
        _neuralNetworkRepository = neuralNetworkRepository;
        _userRepositroy = userRepositroy;
    }
    [HttpPost("feedforward")]
    public async Task<double> FeedFroward(SymptomesDTO req)
    {
        var rabbitMqDto = new RabbitMqDto<SymptomesDTO>()
        {
            TriggeredBy = _userRepositroy.CurrentUser,
            Data = req
        };
        await _neuralNetworkRepository.SendSymptomsToQueue(rabbitMqDto);

        return await Task.FromResult(1);
    }
    
    [HttpPost("recurrent")]
    public async Task<double> Recurrent(SymptomesDTO req)
    {
        return await Task.FromResult(1);
    }
}