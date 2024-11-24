using MongoDB.Bson;

namespace ReportEase.api.Models
{

    public class FoodWasteReport
    {
        public ObjectId Id { get; set; }
        public ObjectId? FoodItemId { get; set; }
        public string CustomFoodItem { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime ReportDate { get; set; }
        public List<ObjectId> PhotoIds { get; set; } // References photos attached to the report
    }


}