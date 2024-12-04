using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.SaleNumber).NotEmpty();
            RuleFor(sale => sale.CustomerId).NotEmpty();
            RuleFor(sale => sale.BranchId).NotEmpty();
            RuleForEach(sale => sale.Items).SetValidator(new CreateSaleItemValidator());
        }
    }
}

public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemValidator()
    {
        RuleFor(item => item.ProductId).NotEmpty();
        RuleFor(item => item.Quantity).GreaterThan(0);
        RuleFor(item => item.UnitPrice).GreaterThan(0);
    }
}