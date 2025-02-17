using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<bool>
{
    public Guid SaleId { get; set; }
    public List<SaleItemModel> Items { get; set; }
}
