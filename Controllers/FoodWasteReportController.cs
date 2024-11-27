using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ReportEase.api.DTOs;
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

    /*
    [HttpPost]
    public async Task<IActionResult> CreateReport([FromForm] FoodWasteReportDto reportDto)
    {
        try
        {
            Console.WriteLine(JsonSerializer.Serialize(reportDto));
            if (reportDto == null || string.IsNullOrEmpty(reportDto.ReportJson))
                return BadRequest("Report data is required.");

            var report = JsonSerializer.Deserialize<FoodWasteReport>(reportDto.ReportJson);

            if (report == null)
                return BadRequest("Invalid report data.");

            if (reportDto.File != null && reportDto.File.Length > 0)
            {
                using var stream = reportDto.File.OpenReadStream();
                var photoId = await _photoService.UploadFileAsync(stream, reportDto.File.FileName, reportDto.File.ContentType);

                report.PhotoIds = new List<string> { photoId.ToString() };
            }

            await _foodWasteReportService.CreateReportAsync(report);
            return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
        }
        catch
        {
            return BadRequest("incorrect input");
        }
    }
    */
    
    [HttpPost]
    public async Task<IActionResult> CreateReport([FromForm] FoodWasteReportDto reportDto)
    {
       
            Console.WriteLine((reportDto.ReportJson));
            var report = JsonSerializer.Deserialize<FoodReportCreateDTO>(reportDto.ReportJson);
            Console.WriteLine(report.ToJson());
            var file = reportDto.File;
            if (report != null && file.Length > 0)
            {
               var createdReport = await _foodWasteReportService.CreateReportAsync(report, file);
               return createdReport == null ? NotFound() : Ok(createdReport);
            }

            return BadRequest("Invalid report data.");
    
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetReportById(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ID format.");

        var report = await _foodWasteReportService.GetReportByIdAsync(id);
        return Ok(report);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReport(string id, [FromBody] FoodWasteReport report)
    {
        /*
        if (!ObjectId.TryParse(id, out var objectId) || objectId != report.Id)
            return BadRequest("ID mismatch or invalid format.");
            */

        await _foodWasteReportService.UpdateReportAsync(report);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReport(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return BadRequest("Invalid ID format.");

        await _foodWasteReportService.DeleteReportAsync(id);
        return NoContent();
    }
}