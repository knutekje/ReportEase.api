namespace ReportEase.api.DTOs;
public class FoodWasteReportDto
{
    public IFormFile File { get; set; } // For the uploaded photo
    public string ReportJson { get; set; } // JSON-encoded FoodWasteReport data
}