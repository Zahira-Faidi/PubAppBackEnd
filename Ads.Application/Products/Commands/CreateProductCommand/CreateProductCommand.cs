﻿using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Commands.CreateProductCommand
{
    public record CreateProductCommand(
        string Name,
        string Image,
        double Price,
        int Quantity,
        int cpc,
        string CategoryId
        ) : IRequest<ProductEntity>;

}
