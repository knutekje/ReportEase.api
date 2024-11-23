using MongoDB.Bson;
using ReportEase.api.Models;
using ReportEase.api.Repositories;

namespace ReportEase.api.Services
{


    public class FoodItemService
    {
        private readonly FoodItemRepository _repository;

        public FoodItemService(FoodItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FoodItem>> GetAllItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<FoodItem> GetItemByIdAsync(ObjectId id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Food item with ID {id} not found.");
            }

            return item;
        }

        public async Task CreateItemAsync(FoodItem item)
        {
            await _repository.CreateAsync(item);
        }

        public async Task UpdateItemAsync(FoodItem item)
        {
            await _repository.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(ObjectId id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}