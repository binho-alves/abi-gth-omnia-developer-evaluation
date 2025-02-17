using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

public class SaleItemValidator : AbstractValidator<SaleItemModel>
{
    public SaleItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
    }
}
