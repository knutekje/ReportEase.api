using MongoDB.Driver;
using ReportEase.api.Models;

namespace ReportEase.api.Repositories;

public class TemperatureReadingRepository
{
    private readonly IMongoCollection<TemperatureReading> _readings;

    public TemperatureReadingRepository(MongoDbContext context)
    {
        _readings = context.GetCollection<TemperatureReading>("TemperatureReadings");
    }

    public async Task<List<TemperatureReading>> GetAllAsync()
    {
        return await _readings.Find(_ => true).ToListAsync();
    }

    public async Task<TemperatureReading> GetByIdAsync(string id)
    {
        return await _readings.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(TemperatureReading reading)
    {
        await _readings.InsertOneAsync(reading);
    }

    public async Task UpdateAsync(TemperatureReading reading)
    {
        await _readings.ReplaceOneAsync(r => r.Id == reading.Id, reading);
    }

    public async Task DeleteAsync(string id)
    {
        await _readings.DeleteOneAsync(r => r.Id == id);
    }
}
