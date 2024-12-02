using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DailyTemperatureRecord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime Date { get; set; } 
    public string SubmittedBy { get; set; } 
    public List<MeasuringPointReading> MeasuringPoints { get; set; } = new List<MeasuringPointReading>();
}

public class MeasuringPointReading
{
    public string Name { get; set; }
    public double Temperature { get; set; } 
    public bool WithinLimits { get; set; } 
}