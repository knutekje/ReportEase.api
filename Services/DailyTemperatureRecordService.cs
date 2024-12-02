using ReportEase.api.Models;
using ReportEase.api.Repositories;

public class DailyTemperatureRecordService
{
    private readonly DailyTemperatureRecordRepository _recordRepository;
    private readonly MeasuringPointRepository _pointRepository;

    public DailyTemperatureRecordService(
        DailyTemperatureRecordRepository recordRepository,
        MeasuringPointRepository pointRepository)
    {
        _recordRepository = recordRepository;
        _pointRepository = pointRepository;
    }

    public async Task AddUnitAsync(string recordId, TemperaturePoint newUnit)
    {
        if (string.IsNullOrEmpty(newUnit.Unit))
        {
            throw new ArgumentException("Unit name cannot be empty.");
        }

        await _recordRepository.AddUnitAsync(recordId, newUnit);
    }

    public async Task RemoveUnitAsync(string recordId, string unitName)
    {
        if (string.IsNullOrEmpty(unitName))
        {
            throw new ArgumentException("Unit name cannot be empty.");
        }

        await _recordRepository.RemoveUnitAsync(recordId, unitName);
    }
    public async Task AddUnitToRecordAsync(string recordId, TemperaturePoint point)
    {
        var record = await _recordRepository.GetByIdAsync(recordId);
        if (record == null)
        {
            throw new KeyNotFoundException($"Record with ID {recordId} not found.");
        }

        await _recordRepository.AddUnitAsync(recordId, point);
    }

    public async Task RemoveUnitFromRecordAsync(string recordId, string unitName)
    {
        var record = await _recordRepository.GetByIdAsync(recordId);
        if (record == null)
        {
            throw new KeyNotFoundException($"Record with ID {recordId} not found.");
        }

        await _recordRepository.RemoveUnitAsync(recordId, unitName);
    }
    
    public async Task<DailyTemperatureRecord> GetRecordByIdAsync(string id)
    {
        var record = await _recordRepository.GetByIdAsync(id);
        if (record == null)
        {
            throw new KeyNotFoundException($"Record with ID {id} not found.");
        }
        return record;
    }

    
    public async Task CreateRecordAsync(DailyTemperatureRecord record)
    {
        record.Date = record.Date.Date; // Normalize date
        await _recordRepository.CreateAsync(record);
    }

   
    public async Task UpdateRecordAsync(DailyTemperatureRecord record)
    {
        var existingRecord = await _recordRepository.GetByIdAsync(record.Id);
        if (existingRecord == null)
        {
            throw new KeyNotFoundException($"Record with ID {record.Id} not found.");
        }

        await _recordRepository.UpdateAsync(record);
    }

   
    public async Task DeleteRecordAsync(string id)
    {
        var existingRecord = await _recordRepository.GetByIdAsync(id);
        if (existingRecord == null)
        {
            throw new KeyNotFoundException($"Record with ID {id} not found.");
        }

        await _recordRepository.DeleteAsync(id);
    }
    public async Task<List<DailyTemperatureRecord>> GetAllRecordsAsync()
    {
        return await _recordRepository.GetAllAsync();
    }
    public async Task<DailyTemperatureRecord> CreateDailyRecordAsync(DailyTemperatureRecord record)
    {
        var activePoints = await _pointRepository.GetAllAsync();
        record.MeasuringPoints = activePoints
            .Where(p => p.IsEnabled)
            .Select(p => new MeasuringPointReading
            {
                Name = p.Name,
                Temperature = 0, 
                WithinLimits = true 
            })
            .ToList();

        await _recordRepository.CreateAsync(record);
        return record;
    }
    
}