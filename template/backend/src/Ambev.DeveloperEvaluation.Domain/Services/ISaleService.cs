using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Services;

public interface ISaleService
{
    Task<Sale> CreateSaleAsync(Sale sale);
    Task<Sale?> GetSaleByIdAsync(Guid id);
    Task<List<Sale>> GetAllSalesAsync();
    Task<Sale> UpdateSaleAsync(Sale sale);
    Task<bool> DeleteSaleAsync(Guid id);
}
