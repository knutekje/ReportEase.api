using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReportEase.api.Models;
using ReportEase.api.Repositories;
using ReportEase.api.Services;

namespace ReportEase.api.Controllers;

[ApiController]
[Route("api/measuring-points")]
public class MeasuringPointController : ControllerBase
{
    private readonly MeasuringPointService _service;

    public MeasuringPointController(MeasuringPointService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPoints()
    {
        var points = await _service.GetAllPointsAsync();
        return Ok(points);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePoint([FromBody] MeasuringPoint point)
    {
        await _service.CreatePointAsync(point);
        return CreatedAtAction(nameof(GetAllPoints), new { id = point.Id }, point);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePoint(string id, [FromBody] MeasuringPoint point)
    {
        if (id != point.Id)
        {
            return BadRequest("ID mismatch.");
        }

        await _service.UpdatePointAsync(point);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePoint(string id)
    {
        await _service.DeletePointAsync(id);
        return NoContent();
    }
}
