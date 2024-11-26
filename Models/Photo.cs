using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReportEase.api.Models;
public class Photo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 
    public string Filename { get; set; }
    public string ContentType { get; set; }
    public ObjectId? AssociatedFoodWasteReportId { get; set; } 
    public ObjectId? AssociatedDiscrepancyId { get; set; }
    public DateTime UploadedAt { get; set; }
}