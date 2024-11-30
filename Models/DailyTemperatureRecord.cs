using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class DailyTemperatureRecord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime Date { get; set; } // The date for the temperature readings

    public string SubmittedBy { get; set; } // Who submitted the readings

    public List<TemperaturePoint> TemperaturePoints { get; set; } = new List<TemperaturePoint>();
}
