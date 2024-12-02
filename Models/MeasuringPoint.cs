namespace ReportEase.api.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MeasuringPoint
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; } 
    public bool IsEnabled { get; set; } = true; 
}
