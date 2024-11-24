using MongoDB.Bson;

public class Photo
{
    public ObjectId Id { get; set; } 
    public string Filename { get; set; }
    public string ContentType { get; set; }
    public ObjectId? AssociatedFoodWasteReportId { get; set; } 
    public ObjectId? AssociatedDiscrepancyId { get; set; }
    public DateTime UploadedAt { get; set; }
}