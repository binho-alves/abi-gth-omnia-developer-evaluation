using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _saleRepository.DeleteAsync(request.Id);
            if (!deleted)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            return new DeleteSaleResponse { Success = true };
        }
    }
}
