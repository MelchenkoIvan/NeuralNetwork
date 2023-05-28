using System.Text.Json;
using AutoMapper;
using FeedForwardNeuralNetwork;
using Microsoft.Extensions.Options;
using NeuralNetworkCore.Models;
using NeuralNetworkCore.Models.Settings;
using NeuralNetworkDatabase;
using NeuralNetworkDatabase.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NeuralNetworkReceiver.Receivers;

public class Receiver : IReceiver
{
    private readonly ISymptomsService _symptomsService;
    private readonly IUserService _userService;
    private readonly RabbitMqSettings _rabbitMqSettings;
    private readonly IMapper _mapper;
    public Receiver(ISymptomsService symptomsService, IOptions<RabbitMqSettings> rabbitMqSettings, IUserService userService, IMapper mapper)
    {
        _symptomsService = symptomsService;
        _userService = userService;
        _mapper = mapper;
        _rabbitMqSettings = rabbitMqSettings.Value;
    }
    
    public void Receive()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqSettings.HostName,
            UserName = _rabbitMqSettings.UserName,
            Password = _rabbitMqSettings.Password,
            VirtualHost = _rabbitMqSettings.VirtualHost,
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(_rabbitMqSettings.Queue, durable: false,
            exclusive: false,
            autoDelete:false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            using MemoryStream ms = new MemoryStream(body);
            
            var rabbitMqDto = JsonSerializer.Deserialize<RabbitMqSymptomsDTO>(ms);
            if (rabbitMqDto is null)
                throw new Exception("Cant read data from queue");
            
            double result;
            if (rabbitMqDto.NnType == NNTypes.FFNN)
            {
                var inputSignals = rabbitMqDto!.Data.GetInputSignals();
                result = Executor.Predict(inputSignals);
            }
            else
            {
                result = RecurrentNeuralNetwork.Executor.Predict(CreateInputSignalsForRnn(rabbitMqDto));
            }

            var user = _userService.GetUser(rabbitMqDto.TriggeredBy.UserName).GetAwaiter().GetResult();
            
            var symptoms = new Symptoms
            {
                User = user,
                UserIdentity = user.UserIdentity,
                Age = rabbitMqDto.Data.Age,
                Sex = rabbitMqDto.Data.Sex,
                Cp = rabbitMqDto.Data.Cp,
                Trestbps = rabbitMqDto.Data.Trestbps,
                Chol = rabbitMqDto.Data.Chol,
                Fbs = rabbitMqDto.Data.Fbs,
                Restecg = rabbitMqDto.Data.Restecg,
                Thalach = rabbitMqDto.Data.Thalach,
                Exang = rabbitMqDto.Data.Exang,
                Oldpeak = rabbitMqDto.Data.Oldpeak,
                Slope = rabbitMqDto.Data.Slope,
                Ca = rabbitMqDto.Data.Ca,
                Thal = rabbitMqDto.Data.Thal,
                Result = result,
                NNType = (int)rabbitMqDto.NnType
            };
            _symptomsService.AddSymptoms(symptoms).GetAwaiter().GetResult();
            Console.WriteLine($"Result for user = ${rabbitMqDto.TriggeredBy.UserName} is ${result} ");
        };
        
        channel.BasicConsume(queue: _rabbitMqSettings.Queue,
            autoAck: true,
            consumer: consumer);
        
        Console.WriteLine($"Press Enter to finish.");
        Console.ReadLine();
    }
    
    private List<double[,]> CreateInputSignalsForRnn(RabbitMqSymptomsDTO rabbitMqDto)
    {
        var previousPredictions = _symptomsService.GetSymptoms(rabbitMqDto.TriggeredBy.UserIdentity).GetAwaiter().GetResult();
        var previousMappedResults = _mapper.Map<List<SymptomsDTO>>(previousPredictions);
        
        var inputSignals = new List<double[,]>();
        foreach (var prevResult in previousMappedResults)
        {
            inputSignals.Add(prevResult.GetInputSignals());
        }
        inputSignals.Add(rabbitMqDto.Data.GetInputSignals());
        return inputSignals;
    }
}