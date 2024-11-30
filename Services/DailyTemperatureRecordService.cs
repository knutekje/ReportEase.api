public class DailyTemperatureRecordService
{
    private readonly DailyTemperatureRecordRepository _repository;

    public DailyTemperatureRecordService(DailyTemperatureRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DailyTemperatureRecord>> GetAllRecordsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<DailyTemperatureRecord> GetRecordByDateAsync(DateTime date)
    {
        return await _repository.GetByDateAsync(date) ?? throw new KeyNotFoundException($"No record found for date {date.ToShortDateString()}.");
    }

    public async Task CreateRecordAsync(DailyTemperatureRecord record)
    {
        record.Date = record.Date.Date; // Ensure date is set without time
        await _repository.CreateAsync(record);
    }

    public async Task UpdateRecordAsync(DailyTemperatureRecord record)
    {
        var existing = await _repository.GetByDateAsync(record.Date);
        if (existing == null)
        {
            throw new KeyNotFoundException($"No record found for date {record.Date.ToShortDateString()}.");
        }

        await _repository.UpdateAsync(record);
    }

    public async Task DeleteRecordAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}
