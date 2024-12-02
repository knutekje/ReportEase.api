using MongoDB.Driver;
using ReportEase.api.Models;
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
    public async Task AddUnitAsync(string recordId, TemperaturePoint point)
    {
        /*var filter = Builders<DailyTemperatureRecord>.Filter.Eq(r => r.Id, recordId);
        var update = Builders<DailyTemperatureRecord>.Update.AddToSet(r => r.TemperaturePoints, point);
        await _records.UpdateOneAsync(filter, update);*/
        throw new NotImplementedException();
    }
    

    public async Task RemoveUnitAsync(string recordId, string unitName)
    {
        /*var filter = Builders<DailyTemperatureRecord>.Filter.Eq(r => r.Id, recordId);
        var update = Builders<DailyTemperatureRecord>.Update.PullFilter(
            r => r.TemperaturePoints,
            point => point.Unit == unitName
        );
        await _records.UpdateOneAsync(filter, update);*/
        throw new NotImplementedException();
    }
    public async Task<DailyTemperatureRecord> GetByIdAsync(string id)
    {
        var filter = Builders<DailyTemperatureRecord>.Filter.Eq(r => r.Id, id);
        return await _records.Find(filter).FirstOrDefaultAsync();
    }
    
    
    
    
}