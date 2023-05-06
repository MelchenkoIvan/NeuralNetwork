using NeuralNetworkCore.Models;

namespace NeuralNetworkCore;

public interface INeuralNetworkRepository
{
     Task SendSymptomsToQueue(RabbitMqDto<SymptomesDTO> symptoms);
}