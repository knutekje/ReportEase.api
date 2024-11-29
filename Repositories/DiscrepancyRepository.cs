using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportEase.api.Repositories;

public class DiscrepancyRepository
{
    private readonly IMongoCollection<Discrepancy> _discrepancies;

    public DiscrepancyRepository(MongoDbContext context)
    {
        _discrepancies = context.GetCollection<Discrepancy>("Discrepancies");
    }

    public async Task CreateAsync(Discrepancy discrepancy)
    {
         await _discrepancies.InsertOneAsync(discrepancy);
     
    }

    public async Task<Discrepancy> GetByIdAsync(string id)
    {
        return await _discrepancies.Find(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Discrepancy>> GetAllDiscReports()
    {
        return await _discrepancies.Find(Builders<Discrepancy>.Filter.Empty).ToListAsync();
        
    }
}
