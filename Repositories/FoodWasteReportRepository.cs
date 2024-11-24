using MongoDB.Bson;
using MongoDB.Driver;
using ReportEase.api.Models;
using ReportEase.api.Repositories;


namespace ReportEase.api.Repositories;

public class FoodWasteReportRepository
{
    private readonly IMongoCollection<FoodWasteReport> _reports;

    public FoodWasteReportRepository(MongoDbContext context)
    {
        _reports = context.GetCollection<FoodWasteReport>("FoodWasteReports");
    }

    public async Task<List<FoodWasteReport>> GetAllAsync()
    {
        return await _reports.Find(Builders<FoodWasteReport>.Filter.Empty).ToListAsync();
    }

    public async Task<FoodWasteReport> GetByIdAsync(ObjectId id)
    {
        return await _reports.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(FoodWasteReport report)
    {
        await _reports.InsertOneAsync(report);
    }

    public async Task UpdateAsync(FoodWasteReport report)
    {
        await _reports.ReplaceOneAsync(r => r.Id == report.Id, report);
    }

    public async Task DeleteAsync(ObjectId id)
    {
        await _reports.DeleteOneAsync(r => r.Id == id);
    }
}