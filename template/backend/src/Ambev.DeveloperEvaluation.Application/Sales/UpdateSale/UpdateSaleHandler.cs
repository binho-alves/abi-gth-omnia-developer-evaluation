using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;

    public UpdateSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        // Recuperar a venda existente
        var sale = await _saleRepository.GetByIdAsync(request.Id);
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");
        }

        // Atualizar propriedades
        sale.SaleNumber = request.SaleNumber;
        sale.CustomerId = request.CustomerId;
        sale.BranchId = request.BranchId;

        // Atualizar itens da venda
        sale.Items.Clear();
        foreach (var item in request.Items)
        {
            sale.Items.Add(new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = 0, // Calculado posteriormente
            });
        }

        // Recalcular o total da venda
        sale.TotalAmount = sale.Items.Sum(item => (item.UnitPrice - item.Discount) * item.Quantity);

        // Salvar as alterações
        await _saleRepository.UpdateAsync(sale);

        // Retornar o resultado
        return new UpdateSaleResult
        {
            Id = sale.Id,
            SaleNumber = sale.SaleNumber,
            CustomerId = sale.CustomerId,
            BranchId = sale.BranchId,
            TotalAmount = sale.TotalAmount
        };
    }
}
