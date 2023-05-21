namespace NeuralNetworkCore.Models;

public class RabbitMqSymptomsDto
{
    public UserDTO TriggeredBy { get; set; }
    
    public NNTypes NnType { get; set; }
    
    public SymptomsDTO Data { get; set; }
}