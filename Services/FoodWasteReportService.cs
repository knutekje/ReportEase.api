using MongoDB.Bson;
using ReportEase.api.Models;
using ReportEase.api.Repositories;
using ReportEase.api.Services;

namespace ReportEase.api.Services
{

    public class FoodWasteReportService
    {
        private readonly FoodWasteReportRepository _repository;
        private readonly FoodItemService _foodItemService;

        public FoodWasteReportService(FoodWasteReportRepository repository, FoodItemService foodItemService)
        {
            _repository = repository;
            _foodItemService = foodItemService;
        }

        public async Task<List<FoodWasteReport>> GetAllReportsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<FoodWasteReport> GetReportByIdAsync(ObjectId id)
        {
            var report = await _repository.GetByIdAsync(id);
            if (report == null)
            {
                throw new KeyNotFoundException($"Report with ID {id} not found.");
            }

            return report;
        }

        public async Task CreateReportAsync(FoodWasteReport report)
        {
            // Validate Quantity
            if (report.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            // Validate FoodItemId if present
            if (report.FoodItemId.HasValue)
            {
                var foodItem = await _foodItemService.GetItemByIdAsync(report.FoodItemId.Value);
                if (foodItem == null)
                {
                    throw new ArgumentException($"Food item with ID {report.FoodItemId} not found.");
                }
            }

            // Insert report
            await _repository.CreateAsync(report);
        }

        public async Task UpdateReportAsync(FoodWasteReport report)
        {
            await _repository.UpdateAsync(report);
        }

        public async Task DeleteReportAsync(ObjectId id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}