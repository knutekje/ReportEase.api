namespace ReportEase.api.DTOs;

public class FoodReportCreateDTO
{
    
    public string? FoodItemId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Value { get; set; }
    public string Description { get; set; }
    public string Department { get; set; }
    public string SubmittedBy { get; set; }
}