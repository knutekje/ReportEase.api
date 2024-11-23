using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ReportEase.api.Models;
using ReportEase.api.Services;

[ApiController]
[Route("api/food-items")]
public class FoodItemController : ControllerBase
{
    private readonly FoodItemService _foodItemService;

    public FoodItemController(FoodItemService foodItemService)
    {
        _foodItemService = foodItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFoodItems()
    {
        var items = await _foodItemService.GetAllItemsAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFoodItemById(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ID format.");

        var item = await _foodItemService.GetItemByIdAsync(objectId);
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFoodItem([FromBody] FoodItem foodItem)
    {
        if (foodItem == null)
            return BadRequest("Invalid data.");

        await _foodItemService.CreateItemAsync(foodItem);
        return CreatedAtAction(nameof(GetFoodItemById), new { id = foodItem.Id }, foodItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFoodItem(string id, [FromBody] FoodItem foodItem)
    {
        if (!ObjectId.TryParse(id, out var objectId) || objectId != foodItem.Id)
            return BadRequest("ID mismatch or invalid format.");

        await _foodItemService.UpdateItemAsync(foodItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFoodItem(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ID format.");

        await _foodItemService.DeleteItemAsync(objectId);
        return NoContent();
    }
}
