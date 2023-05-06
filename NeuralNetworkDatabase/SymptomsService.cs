using NeuralNetworkDatabase.Entities;

namespace NeuralNetworkDatabase;

public class SymptomsService : ISymptomsService
{
    private readonly NeuralNetworkDbContext _dbContext;

    public SymptomsService(NeuralNetworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddSymptoms(Symptoms symptoms)
    {
        await _dbContext.AddAsync(symptoms);
        await _dbContext.SaveChangesAsync();
    }
}