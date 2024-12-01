using MongoDB.Driver;
using ReportEase.api.Repositories;

public class DailyTemperatureRecordRepository
{
    private readonly IMongoCollection<DailyTemperatureRecord> _records;

    public DailyTemperatureRecordRepository(MongoDbContext context)
    {
        _records = context.GetCollection<DailyTemperatureRecord>("DailyTemperatureRecords");
    }

    public async Task<List<DailyTemperatureRecord>> GetAllAsync()
    {
        return await _records.Find(_ => true).ToListAsync();
    }

    public async Task<DailyTemperatureRecord> GetByDateAsync(DateTime date)
    {
        return await _records.Find(r => r.Date == date.Date).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(DailyTemperatureRecord record)
    {
        await _records.InsertOneAsync(record);
    }

    public async Task UpdateAsync(DailyTemperatureRecord record)
    {
        await _records.ReplaceOneAsync(r => r.Id == record.Id, record);
    }

    public async Task DeleteAsync(string id)
    {
        await _records.DeleteOneAsync(r => r.Id == id);
    }
}