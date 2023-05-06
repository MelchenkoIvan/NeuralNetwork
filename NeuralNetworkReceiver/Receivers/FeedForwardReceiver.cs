using System.Text.Json;
using FeedForwardNeuralNetwork;
using NeuralNetworkCore.Models;
using NeuralNetworkDatabase;
using NeuralNetworkDatabase.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NeuralNetworkReceiver.Receivers;

public class FeedForwardReceiver : IReceiver
{
    private readonly ISymptomsService _symptomsService;
    public FeedForwardReceiver(ISymptomsService symptomsService)
    {
        _symptomsService = symptomsService;
    }
    
    public Task Receive()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "mypass",
            VirtualHost = "/"
        };
        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.QueueDeclare("symptoms", durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            using MemoryStream ms = new MemoryStream(body);
            var rabbitMqDto = JsonSerializer.Deserialize<RabbitMqDto<SymptomesDTO>>(ms);
            var result = Executor.Predict(rabbitMqDto.Data.GetInputSignals());
            var symptoms = new Symptoms
            {
                UserIdentity = rabbitMqDto.TriggeredBy.Id,
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
                NNType = (int)NNTypes.FFNN
            };
            _symptomsService.AddSymptoms(symptoms);
            Console.WriteLine($"Result for user = ${rabbitMqDto.TriggeredBy.UserName} is ${result} ");
        };
        channel.BasicConsume(queue: "symptoms",
            autoAck: true,
            consumer: consumer);
        
        return Task.CompletedTask;
    }
}