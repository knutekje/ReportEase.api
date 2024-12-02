using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportEase.api.DTOs;
using ReportEase.api.Models;
using ReportEase.api.Services;

[ApiController]
[Route("api/daily-temperature-records")]
public class DailyTemperatureRecordController : ControllerBase
{
    private readonly DailyTemperatureRecordService _service;

    public DailyTemperatureRecordController(DailyTemperatureRecordService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecords()
    {
        var records = await _service.GetAllRecordsAsync();
        return Ok(records);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecordById(string id)
    {
        try
        {
            var record = await _service.GetRecordByIdAsync(id);
            return Ok(record);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecord([FromBody] DailyTemperatureRecordCreateDTO dto)
    {
        try
        {
            var record = new DailyTemperatureRecord
            {
                Date = dto.Date,
                SubmittedBy = dto.SubmittedBy,
                MeasuringPoints = dto.MeasuringPoints.Select(mp => new MeasuringPointReading
                {
                    Name = mp.Name,
                    Temperature = mp.Temperature,
                    WithinLimits = mp.WithinLimits
                }).ToList()
            };

            await _service.CreateRecordAsync(record);
            return CreatedAtAction(nameof(GetRecordById), new { id = record.Id }, record);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecord(string id, [FromBody] DailyTemperatureRecord record)
    {
        if (id != record.Id)
        {
            return BadRequest("ID mismatch.");
        }

        try
        {
            await _service.UpdateRecordAsync(record);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecord(string id)
    {
        try
        {
            await _service.DeleteRecordAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id}/units")]
    public async Task<IActionResult> AddUnit(string id, [FromBody] TemperaturePoint point)
    {
        try
        {
            await _service.AddUnitToRecordAsync(id, point);
            return Ok("Unit added successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}/units/{unitName}")]
    public async Task<IActionResult> RemoveUnit(string id, string unitName)
    {
        try
        {
            await _service.RemoveUnitFromRecordAsync(id, unitName);
            return Ok("Unit removed successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
