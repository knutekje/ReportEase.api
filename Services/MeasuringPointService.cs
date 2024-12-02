using ReportEase.api.Controllers;
using ReportEase.api.Models;
using ReportEase.api.Repositories;

namespace ReportEase.api.Services;

public class MeasuringPointService
{
    private readonly MeasuringPointRepository _repository;

    public MeasuringPointService(MeasuringPointRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MeasuringPoint>> GetAllPointsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task CreatePointAsync(MeasuringPoint point)
    {
        await _repository.CreateAsync(point);
    }

    public async Task UpdatePointAsync(MeasuringPoint point)
    {
        await _repository.UpdateAsync(point);
    }

    public async Task DeletePointAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}
