using System.Text;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Options;
using NeuralNetworkCore.Models;
using NeuralNetworkCore.Models.Settings;
using NeuralNetworkDatabase;
using RabbitMQ.Client;

namespace NeuralNetworkCore;

public class NeuralNetworkRepository : INeuralNetworkRepository
{
    private readonly RabbitMqSettings _rabbitMqSettings;
    private readonly ISymptomsService _symptomsService;
    private readonly IMapper _mapper;

    public NeuralNetworkRepository(IOptions<RabbitMqSettings> rabbitMqSettings,
        ISymptomsService symptomsService, IMapper mapper)
    {
        _symptomsService = symptomsService;
        _mapper = mapper;
        _rabbitMqSettings = rabbitMqSettings.Value;
    }

    public Task SendSymptomsToQueue(RabbitMqSymptomsDTO data)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqSettings.HostName,
            UserName = _rabbitMqSettings.UserName,
            Password = _rabbitMqSettings.Password,
            VirtualHost = _rabbitMqSettings.VirtualHost
        };
        using var connection = factory.CreateConnection();
        using var chanel = connection.CreateModel();

        chanel.QueueDeclare(_rabbitMqSettings.Queue, durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var jsonString = JsonSerializer.Serialize(data);
        var body = Encoding.UTF8.GetBytes(jsonString);
        
        chanel.BasicPublish("",_rabbitMqSettings.Queue, body:body);

        return Task.CompletedTask;
    }

    public async Task<List<SymptomsWithResultDTO>> GetSymptoms(int userId)
    {
        var symptomsWithResult = await _symptomsService.GetSymptoms(userId);
        return _mapper.Map<List<SymptomsWithResultDTO>>(symptomsWithResult);
    }
}