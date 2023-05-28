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
    public async Task FeedFroward(SymptomsDTO req)
    {
        var rabbitMqDto = new RabbitMqSymptomsDTO()
        {
            TriggeredBy = _userRepositroy.CurrentUser,
            NnType = NNTypes.FFNN,
            Data = req
        };
        await _neuralNetworkRepository.SendSymptomsToQueue(rabbitMqDto);
        
    }
    
    [HttpPost("recurrent")]
    public async Task Recurrent(SymptomsDTO req)
    {
        var rabbitMqDto = new RabbitMqSymptomsDTO()
        {
            TriggeredBy = _userRepositroy.CurrentUser,
            NnType = NNTypes.RNN,
            Data = req
        };
        await _neuralNetworkRepository.SendSymptomsToQueue(rabbitMqDto);
    }
    
    [HttpPost("list")]
    public async Task<List<SymptomsWithResultDTO>> GetResults()
    {
       return await _neuralNetworkRepository.GetSymptoms(_userRepositroy.CurrentUser.UserIdentity);
    }
}