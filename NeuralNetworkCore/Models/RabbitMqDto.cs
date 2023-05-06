namespace NeuralNetworkCore.Models;

public class RabbitMqDto<T>
{
    public UserDTO TriggeredBy { get; set; }

    public T Data { get; set; }
}