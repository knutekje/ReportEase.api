using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/temperature-records")]
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

    [HttpGet("{date}")]
    public async Task<IActionResult> GetRecordByDate(string date)
    {
        if (!DateTime.TryParse(date, out var parsedDate))
        {
            return BadRequest("Invalid date format.");
        }

        try
        {
            var record = await _service.GetRecordByDateAsync(parsedDate);
            return Ok(record);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecord([FromBody] DailyTemperatureRecord record)
    {
        await _service.CreateRecordAsync(record);
        return CreatedAtAction(nameof(GetRecordByDate), new { date = record.Date.ToShortDateString() }, record);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecord(string id, [FromBody] DailyTemperatureRecord record)
    {
        if (id != record.Id)
        {
            return BadRequest("ID mismatch.");
        }

        await _service.UpdateRecordAsync(record);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecord(string id)
    {
        await _service.DeleteRecordAsync(id);
        return NoContent();
    }
}
