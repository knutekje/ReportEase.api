public class DiscrepancyService
{
    private readonly DiscrepancyRepository _repository;

    public DiscrepancyService(DiscrepancyRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateDiscrepancyAsync(Discrepancy discrepancy)
    {
        if (discrepancy == null)
        {
            throw new ArgumentException("Discrepancy cannot be null.");
        }

        discrepancy.ReportedAt = DateTime.UtcNow;
        await _repository.CreateAsync(discrepancy);
    }

    public async Task<List<Discrepancy>> GetAll()
    {
        return await _repository.GetAllDiscReports();
    }

    public async Task<Discrepancy> GetDiscrepancyByIdAsync(string id)
    {
        var discrepancy = await _repository.GetByIdAsync(id);
        if (discrepancy == null)
        {
            throw new KeyNotFoundException($"Discrepancy with ID {id} not found.");
        }

        return discrepancy;
    }
}