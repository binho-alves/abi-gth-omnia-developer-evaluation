﻿using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Domain.Entities.Sale>();
            CreateMap<CreateSaleItemCommand, SaleItem>();
            CreateMap<Domain.Entities.Sale, CreateSaleResult>();
        }
    }
}