using MongoDB.Bson;


namespace ReportEase.api.Models{

public class FoodItem
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; } // e.g., kg, liters, pieces
    public decimal UnitPrice { get; set; } // Price per unit
    public string Url { get; set; } // Optional: Link to item information
}}