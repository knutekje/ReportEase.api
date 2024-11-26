using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReportEase.api.Models
{

    public class FoodWasteReport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? FoodItemId { get; set; }
        public decimal Quantity { get; set; }
        
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime ReportDate { get; set; }
        public string PhotoId { get; set; } 
    }


}