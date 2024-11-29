using Microsoft.AspNetCore.Mvc;
using ReportEase.api.Models;
using ReportEase.api.Services;

namespace ReportEase.api.Controllers;

[ApiController]
[Route("api/temperature-readings")]
public class TemperatureReadingController : ControllerBase
{
    private readonly TemperatureReadingService _service;

    public TemperatureReadingController(TemperatureReadingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReadings()
    {
        var readings = await _service.GetAllReadingsAsync();
        return Ok(readings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReadingById(string id)
    {
        try
        {
            var reading = await _service.GetReadingByIdAsync(id);
            return Ok(reading);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateReading([FromBody] TemperatureReading reading)
    {
        try
        {
            await _service.CreateReadingAsync(reading);
            return CreatedAtAction(nameof(GetReadingById), new { id = reading.Id }, reading);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReading(string id, [FromBody] TemperatureReading reading)
    {
        if (id != reading.Id)
        {
            return BadRequest("ID mismatch.");
        }

        try
        {
            await _service.UpdateReadingAsync(reading);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReading(string id)
    {
        try
        {
            await _service.DeleteReadingAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
