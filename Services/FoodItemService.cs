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

       
        public async Task<PaginatedResult<FoodItemDto>> GetFoodItemsAsync(string search, int page, int limit)
        {
            var items = await _repository.GetPaginatedFoodItemsAsync(search, page, limit);
            var totalCount = await _repository.GetTotalCountAsync(search);

            return new PaginatedResult<FoodItemDto>
            {
                Items = items.Select(f => new FoodItemDto
                {
                    Id = f.Id,
                    Name = f.Produktnavn,
                    Unit = f.Enhetstype
                }).ToList(),
                TotalCount = totalCount,
                HasMore = (page * limit) < totalCount
            };
        }
        
        

        public async Task<FoodItem> GetItemByIdAsync(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item ?? new FoodItem() { Anbrekkspris = 0 };
        }

        public async Task CreateItemAsync(FoodItem item)
        {
            await _repository.CreateAsync(item);
        }

        public async Task UpdateItemAsync(FoodItem item)
        {
            await _repository.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}