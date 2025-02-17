using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly IValidator<Sale> _saleValidator;

    public CreateSaleHandler(ISaleRepository saleRepository, ILogger<CreateSaleHandler> logger, IValidator<Sale> saleValidator)
    {
        _saleRepository = saleRepository;
        _logger = logger;
        _saleValidator = saleValidator;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var saleItems = request.Items.Select(i => new SaleItem(i.ProductId, i.Quantity, i.UnitPrice)).ToList();

        var sale = new Sale(request.CustomerId, saleItems);

        var validationResult = await _saleValidator.ValidateAsync(sale, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        await _saleRepository.CreateAsync(sale);
        _logger.LogInformation("Sale created successfully: {@sale}", sale);

        return new CreateSaleResult
        {
            SaleId = sale.Id,
            SaleNumber = sale.SaleNumber,
            TotalAmount = sale.TotalAmount
        };
    }
}
