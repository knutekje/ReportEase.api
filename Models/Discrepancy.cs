    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Discrepancy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public SeverityLevel Severity { get; set; }

        [BsonRepresentation(BsonType.String)]
        public StatusLevel Status { get; set; }

        public string ReportedBy { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string TempSolution { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? PhotoId { get; set; } // Optional Photo

        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    }



// Enums for Severity and Status
public enum SeverityLevel
{
    Low,
    Normal,
    High,
    Critical
}

public enum StatusLevel
{
    Open,
    NeedForImprovement,
    Improved,
    Closed
}