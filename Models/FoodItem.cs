using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ReportEase.api.Models{

public class FoodItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; } 
    public decimal UnitPrice { get; set; }
    public string Url { get; set; }
}}