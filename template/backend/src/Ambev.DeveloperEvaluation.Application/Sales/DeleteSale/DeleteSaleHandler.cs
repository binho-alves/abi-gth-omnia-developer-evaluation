using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<DeleteSaleHandler> _logger;

    public DeleteSaleHandler(ISaleRepository saleRepository, ILogger<DeleteSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var success = await _saleRepository.DeleteAsync(request.SaleId);
        if (!success)
        {
            _logger.LogWarning("Sale with ID {SaleId} not found for deletion.", request.SaleId);
            return false;
        }

        _logger.LogInformation("Sale with ID {SaleId} deleted successfully.", request.SaleId);
        return true;
    }
}