using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreateSaleHandler(ISaleRepository repository, IMapper mapper, IBus bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Domain.Entities.Sale>(request);
            sale.Date = DateTime.UtcNow;

            foreach (var item in sale.Items)
            {
                if (item.Quantity > 20)
                    throw new ValidationException("Cannot sell more than 20 identical items.");
                if (item.Quantity >= 10)
                    item.Discount = item.UnitPrice * 0.2m;
                else if (item.Quantity >= 4)
                    item.Discount = item.UnitPrice * 0.1m;
            }

            sale.TotalAmount = sale.Items.Sum(i => i.TotalAmount);

            await _repository.AddAsync(sale);
            await _bus.Publish(new SaleCreatedEvent { SaleId = sale.Id });

            return _mapper.Map<CreateSaleResult>(sale);
        }
    }

}
