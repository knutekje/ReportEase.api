using MongoDB.Driver;
using ReportEase.api.Models;

namespace ReportEase.api.Repositories;

public class MeasuringPointRepository
{
    private readonly IMongoCollection<MeasuringPoint> _points;

    public MeasuringPointRepository(MongoDbContext context)
    {
        _points = context.GetCollection<MeasuringPoint>("MeasuringPoints");
    }

    public async Task<List<MeasuringPoint>> GetAllAsync()
    {
        return await _points.Find(_ => true).ToListAsync();
    }

    public async Task<MeasuringPoint> GetByIdAsync(string id)
    {
        return await _points.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(MeasuringPoint point)
    {
        await _points.InsertOneAsync(point);
    }

    public async Task UpdateAsync(MeasuringPoint point)
    {
        await _points.ReplaceOneAsync(p => p.Id == point.Id, point);
    }

    public async Task DeleteAsync(string id)
    {
        await _points.DeleteOneAsync(p => p.Id == id);
    }
}
