using System.Text;
using System.Text.Json;
using NeuralNetworkCore.Models;
using RabbitMQ.Client;

namespace NeuralNetworkCore;

public class NeuralNetworkRepository : INeuralNetworkRepository
{
    public Task SendSymptomsToQueue(RabbitMqDto<SymptomesDTO> data)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "mypass",
            VirtualHost = "/"
        };
        var connection = factory.CreateConnection();

        using var chanel = connection.CreateModel();

        chanel.QueueDeclare("symptoms", durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var jsonString = JsonSerializer.Serialize(data);
        var body = Encoding.UTF8.GetBytes(jsonString);
        
        chanel.BasicPublish("","symptoms", body:body);

        return Task.CompletedTask;
    }
}