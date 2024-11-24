using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ReportEase.api.Models;
using ReportEase.api.Services;

[ApiController]
[Route("api/food-waste-reports")]
public class FoodWasteReportController : ControllerBase
{
    private readonly FoodWasteReportService _foodWasteReportService;
    private readonly PhotoService _photoService;

    public FoodWasteReportController(FoodWasteReportService foodWasteReportService, PhotoService photoService)
    {
        _foodWasteReportService = foodWasteReportService;
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _foodWasteReportService.GetAllReportsAsync();
        return Ok(reports);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReport([FromForm] FoodWasteReportDto reportDto)
    {
        if (reportDto == null || string.IsNullOrEmpty(reportDto.ReportJson))
            return BadRequest("Report data is required.");

        // Deserialize the ReportJson to a FoodWasteReport object
        var report = JsonSerializer.Deserialize<FoodWasteReport>(reportDto.ReportJson);

        if (report == null)
            return BadRequest("Invalid report data.");

        // Handle file upload if a file is provided
        if (reportDto.File != null && reportDto.File.Length > 0)
        {
            using var stream = reportDto.File.OpenReadStream();
            var photoId = await _photoService.UploadPhotoAsync(
                stream, reportDto.File.FileName, reportDto.File.ContentType, report.Id, null);

            report.PhotoIds = new List<ObjectId> { photoId };
        }

        // Save the report
        await _foodWasteReportService.CreateReportAsync(report);
        return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetReportById(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ID format.");

        var report = await _foodWasteReportService.GetReportByIdAsync(objectId);
        return Ok(report);
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