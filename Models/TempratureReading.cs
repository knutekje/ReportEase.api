namespace ReportEase.api.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TemperatureReading
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string SubmittedBy { get; set; } // Person who submitted the reading
    public DateTime TimeRead { get; set; } // Time of the reading
    public DateTime DateRead { get; set; } // Date of the reading
    public string Unit { get; set; } // Unit (e.g., "Walk-in One", "Freezer")
    public double Temperature { get; set; } // Recorded temperature
    public bool WithinLimits { get; set; } // Indicates if the temperature is within limits
}
