using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ReportEase.api.Models;
using ReportEase.api.Services;

[ApiController]
[Route("api/food-waste-reports")]
public class FoodWasteReportController : ControllerBase
{
    private readonly FoodWasteReportService _foodWasteReportService;

    public FoodWasteReportController(FoodWasteReportService foodWasteReportService)
    {
        _foodWasteReportService = foodWasteReportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _foodWasteReportService.GetAllReportsAsync();
        return Ok(reports);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReportById(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ID format.");

        var report = await _foodWasteReportService.GetReportByIdAsync(objectId);
        return Ok(report);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReport([FromBody] FoodWasteReport report)
    {
        if (report == null)
            return BadRequest("Invalid data.");

        await _foodWasteReportService.CreateReportAsync(report);
        return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReport(string id, [FromBody] FoodWasteReport report)
    {
        if (!ObjectId.TryParse(id, out var objectId) || objectId != report.Id)
            return BadRequest("ID mismatch or invalid format.");

        await _foodWasteReportService.UpdateReportAsync(report);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReport(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ID format.");

        await _foodWasteReportService.DeleteReportAsync(objectId);
        return NoContent();
    }
}