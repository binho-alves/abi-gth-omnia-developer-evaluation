using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Sale> CreateSaleAsync(Sale sale)
    {
        // Aplicar regras de negócio
        foreach (var item in sale.Items)
        {
            if (item.Quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            if (item.Quantity >= 10)
                item.Discount = item.UnitPrice * 0.2m;
            else if (item.Quantity >= 4)
                item.Discount = item.UnitPrice * 0.1m;

            item.TotalAmount = (item.UnitPrice - item.Discount) * item.Quantity;
        }

        sale.TotalAmount = sale.Items.Sum(i => i.TotalAmount);
        sale.Date = DateTime.UtcNow;

        // Adicionar ao repositório
        await _saleRepository.AddAsync(sale);
        return sale;
    }

    public async Task<Sale?> GetSaleByIdAsync(Guid id)
    {
        return await _saleRepository.GetByIdAsync(id);
    }

    public async Task<List<Sale>> GetAllSalesAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<Sale> UpdateSaleAsync(Sale sale)
    {
        var existingSale = await _saleRepository.GetByIdAsync(sale.Id);
        if (existingSale == null)
            throw new KeyNotFoundException($"Sale with ID {sale.Id} not found.");

        // Aplicar regras de atualização se necessário
        foreach (var item in sale.Items)
        {
            if (item.Quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            if (item.Quantity >= 10)
                item.Discount = item.UnitPrice * 0.2m;
            else if (item.Quantity >= 4)
                item.Discount = item.UnitPrice * 0.1m;

            item.TotalAmount = (item.UnitPrice - item.Discount) * item.Quantity;
        }

        sale.TotalAmount = sale.Items.Sum(i => i.TotalAmount);

        await _saleRepository.UpdateAsync(sale);
        return sale;
    }

    public async Task<bool> DeleteSaleAsync(Guid id)
    {
        return await _saleRepository.DeleteAsync(id);
    }
}
