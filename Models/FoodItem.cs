using MongoDB.Bson;


namespace ReportEase.api.Models{

public class FoodItem
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; } 
    public decimal UnitPrice { get; set; }
    public string Url { get; set; }
}}