using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> CreateAsync(Sale sale);
    Task<Sale?> GetByIdAsync(Guid id);
    Task<List<Sale>> GetAllAsync();
    Task UpdateAsync(Sale sale);
    Task<bool> DeleteAsync(Guid id);
}
