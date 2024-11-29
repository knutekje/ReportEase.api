using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using ReportEase.api.DTOs;
using ReportEase.api.Models;
using ReportEase.api.Repositories;
using ReportEase.api.Services;

namespace ReportEase.api.Services
{

    public class FoodWasteReportService
    {
        private readonly FoodWasteReportRepository _repository;
        private readonly FoodItemService _foodItemService;
        private readonly PhotoService _photoService;

        public FoodWasteReportService(FoodWasteReportRepository repository, FoodItemService foodItemService, PhotoService photoService)
        {
            _repository = repository;
            _photoService = photoService;
            _foodItemService = foodItemService;
        }

        public async Task<List<FoodWasteReportListDTO>> GetAllReportsAsync()
        {
            var reports =  await _repository.GetAllAsync();
            var result = await Task.WhenAll(reports.Select(async report =>
            {
                var foodItem = await _foodItemService.GetItemByIdAsync(report.FoodItemId); // Example of async operation
                return new FoodWasteReportListDTO
                {
                    Fooditemid = report.FoodItemId,
                    Quantity = report.Quantity,
                    Foodwasteid = report.Id,
                    Description = report.Description,
                    Department = report.Department,
                    Value = (report.Quantity * foodItem.Anbrekkspris),
                    ItemName = foodItem?.Produktnavn, 
                    ReportDate = report.ReportDate,
                    PhotoId = report.PhotoId,
                };
            }));

            return result.ToList();
           
        }

        public async Task<FoodWasteReport> GetReportByIdAsync(string id)
        {
            var report = await _repository.GetByIdAsync(id);
            if (report == null)
            {
                throw new KeyNotFoundException($"Report with ID {id} not found.");
            }

            return report;
        }
        
        public async Task<FoodWasteReport> CreateReportAsync( FoodReportCreateDTO report, IFormFile file)
        {
            ObjectId fileid;
            var  foodItem = await _foodItemService.GetItemByIdAsync(report.FoodItemId ?? throw new InvalidOperationException());
            Console.WriteLine(foodItem.Produktnavn + "NAME");
            await using (var stream = file.OpenReadStream())
            {
                fileid = await _photoService.UploadFileAsync(stream, file.FileName, file.ContentType);
            }

            FoodWasteReport foodWasteReport = new()
            {
                FoodItemId = report.FoodItemId,
                Department = report.Department,
                Quantity = report.Quantity,
                Description = report.Description,
                Value = (report.Quantity * foodItem.Anbrekkspris),
                ReportDate = DateTime.Now,
                PhotoId = fileid.ToString(),
            };
            await _repository.CreateAsync(foodWasteReport);
            return foodWasteReport;

        }
        
        public async Task UpdateReportAsync(FoodWasteReport report)
        {
            await _repository.UpdateAsync(report);
        }

        public async Task DeleteReportAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}