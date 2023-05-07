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
        await _dbContext.Symptoms.AddAsync(symptoms);
        await _dbContext.SaveChangesAsync();
    }
    
    public Task<List<Symptoms>> GetSymptoms(int userId)
    {
        var symptoms = _dbContext.Symptoms.Where(x => x.UserIdentity == userId).ToList();
        return Task.FromResult(symptoms);
    }
}