using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleForEach(s => s.Items).SetValidator(new SaleItemValidator());
    }
}

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(4).WithMessage("A quantidade mínima para aplicar um desconto é 4.")
            .LessThanOrEqualTo(20).WithMessage("Não é permitido vender mais de 20 unidades do mesmo item.");
    }
}
