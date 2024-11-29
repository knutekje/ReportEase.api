using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ReportEase.api.DTOs;
using ReportEase.api.Services;

[ApiController]
[Route("api/discrepancies")]
public class DiscrepancyController : ControllerBase
{
    private readonly DiscrepancyService _service;
    private readonly PhotoService _photoService;

    public DiscrepancyController(DiscrepancyService service, PhotoService photoService)
    {
        _service = service;
        _photoService = photoService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscrepancy([FromForm] JSONFileDTO dto)
    {
       
        Console.WriteLine((dto.ReportJson));
        var report = JsonSerializer.Deserialize<Discrepancy>(dto.ReportJson);
        
        var file = dto.File;
        if (report != null && file.Length > 0)
        {
            await _service.CreateDiscrepancyAsync(report);
            //return createdReport == null ? NotFound() : Ok(createdReport);
        }

        return BadRequest("Invalid report data.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscrepancyById(string id)
    {
        try
        {
            var discrepancy = await _service.GetDiscrepancyByIdAsync(id);
            return Ok(discrepancy);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<List<Discrepancy>> GetAllDiscReport()
    {
        return await _service.GetAll();
    }
}
