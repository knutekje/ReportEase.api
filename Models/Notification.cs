using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public NotificationType Type { get; set; } // Notification, Warning, or Direct
    public string Title { get; set; } // A brief title for the notification
    public string Message { get; set; } // Detailed message

    public string RecipientId { get; set; } // For Direct notifications
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When the notification was created
    public bool IsRead { get; set; } = false; // Status of the notification
}

// Enum for Notification Type
public enum NotificationType
{
    Notification,
    Warning,
    Direct
}