namespace ReportEase.api.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TemperatureReading
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string SubmittedBy { get; set; } 
    public DateTime TimeRead { get; set; } 
    public DateTime DateRead { get; set; } 
    public string Unit { get; set; } 
    public double Temperature { get; set; } 
    public bool WithinLimits { get; set; } 
}
