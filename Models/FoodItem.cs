using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ReportEase.api.Models{

public class FoodItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Varenummer")] 
    public string Varenummer { get; set; } = null!;
    
    [BsonElement("Produktnavn")] 
    public string Produktnavn { get; set; }
    
    [BsonElement("Enhetstype")] 
    public string Enhetstype { get; set; } 
    
    [BsonElement("Anbrekkspris")] 
    public decimal Anbrekkspris { get; set; }
   
}}