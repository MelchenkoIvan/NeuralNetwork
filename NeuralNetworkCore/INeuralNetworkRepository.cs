using NeuralNetworkCore.Models;

namespace NeuralNetworkCore;

public interface INeuralNetworkRepository
{
     Task SendSymptomsToQueue(RabbitMqSymptomsDto symptoms);
     Task<List<ResultDTO>> GetSymptoms(int userId);
}