using NeuralNetworkCore.Models;

namespace NeuralNetworkCore;

public interface INeuralNetworkRepository
{
     Task SendSymptomsToQueue(RabbitMqSymptomsDTO symptoms);
     Task<List<SymptomsWithResultDTO>> GetSymptoms(int userId);
}