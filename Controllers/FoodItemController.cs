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
    public async Task<IActionResult> GetFoodItems([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        var result = await _foodItemService.GetFoodItemsAsync(search, page, limit);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFoodItemById(string id)
    {
        var item = await _foodItemService.GetItemByIdAsync(id);
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
        

        await _foodItemService.UpdateItemAsync(foodItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFoodItem(string id)
    {
       

        await _foodItemService.DeleteItemAsync(id);
        return NoContent();
    }
}
