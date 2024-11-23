using MongoDB.Bson;

namespace ReportEase.api.Models
{

    public class FoodWasteReport
    {
        public ObjectId Id { get; set; }
        public ObjectId? FoodItemId { get; set; } // Reference to predefined FoodItem
        public string CustomFoodItem { get; set; } // For custom item names
        public decimal Quantity { get; set; } // Amount of food wasted
        public string Description { get; set; } // Optional additional information
        public string SubmittedBy { get; set; }
        public DateTime ReportDate { get; set; }
        public List<ObjectId> PhotoIds { get; set; } // References to photos in GridFS
    }
}