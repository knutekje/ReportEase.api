using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public NotificationType Type { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }

    public string RecipientId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
}


public enum NotificationType
{
    Notification,
    Warning,
    Direct
}