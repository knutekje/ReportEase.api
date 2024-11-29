namespace ReportEase.api.DTOs;
public class JSONFileDTO
{
    public IFormFile File { get; set; } // For the uploaded photo
    public string ReportJson { get; set; } // JSON-encoded FoodWasteReport data
}