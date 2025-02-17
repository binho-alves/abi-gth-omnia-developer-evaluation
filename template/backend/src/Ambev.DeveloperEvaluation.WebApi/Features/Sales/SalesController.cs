using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request)
    {
        var result = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetSaleById), new { id = result.SaleId }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleById(Guid id)
    {
        var result = await _mediator.Send(new GetSaleQuery { SaleId = id });
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var success = await _mediator.Send(new DeleteSaleCommand { SaleId = id });
        if (!success) return NotFound();

        return NoContent();
    }
}
