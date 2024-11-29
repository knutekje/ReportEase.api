using ReportEase.api.Models;
using ReportEase.api.Repositories;

namespace ReportEase.api.Services;

public class TemperatureReadingService
{
    private readonly TemperatureReadingRepository _repository;

    public TemperatureReadingService(TemperatureReadingRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TemperatureReading>> GetAllReadingsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TemperatureReading> GetReadingByIdAsync(string id)
    {
        var reading = await _repository.GetByIdAsync(id);
        if (reading == null)
        {
            throw new KeyNotFoundException($"Temperature reading with ID {id} not found.");
        }
        return reading;
    }

    public async Task CreateReadingAsync(TemperatureReading reading)
    {
        if (reading == null)
        {
            throw new ArgumentException("Temperature reading cannot be null.");
        }

        reading.TimeRead = DateTime.UtcNow;
        await _repository.CreateAsync(reading);
    }

    public async Task UpdateReadingAsync(TemperatureReading reading)
    {
        var existing = await _repository.GetByIdAsync(reading.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Temperature reading with ID {reading.Id} not found.");
        }

        await _repository.UpdateAsync(reading);
    }

    public async Task DeleteReadingAsync(string id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Temperature reading with ID {id} not found.");
        }

        await _repository.DeleteAsync(id);
    }
}
