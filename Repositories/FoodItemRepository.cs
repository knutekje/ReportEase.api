using MongoDB.Bson;
using MongoDB.Driver;
using ReportEase.api.Models;
using ReportEase.api.Repositories;

namespace ReportEase.api.Repositories;

public class FoodItemRepository
{
    private readonly IMongoCollection<FoodItem> _foodItems;

    public FoodItemRepository(MongoDbContext context)
    {
        _foodItems = context.GetCollection<FoodItem>("FoodItems");
    }

    public async Task<List<FoodItem>> GetAllAsync()
    {
        return await _foodItems.Find(Builders<FoodItem>.Filter.Empty).ToListAsync();
    }

    public async Task<FoodItem> GetByIdAsync(string id)
    {
        return await _foodItems.Find(f => f.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(FoodItem item)
    {
        await _foodItems.InsertOneAsync(item);
    }

    public async Task UpdateAsync(FoodItem item)
    {
        await _foodItems.ReplaceOneAsync(f => f.Id == item.Id, item);
    }

    public async Task DeleteAsync(string id)
    {
        await _foodItems.DeleteOneAsync(f => f.Id == id);
    }
}