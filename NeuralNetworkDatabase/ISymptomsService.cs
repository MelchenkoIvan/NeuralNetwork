using NeuralNetworkDatabase.Entities;

namespace NeuralNetworkDatabase;

public interface ISymptomsService
{
    Task AddSymptoms(Symptoms user);
    Task<List<Symptoms>> GetSymptoms(int userId);
}