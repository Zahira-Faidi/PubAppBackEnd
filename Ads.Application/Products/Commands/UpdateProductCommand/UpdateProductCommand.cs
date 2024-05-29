using Ads.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ads.Application.Products.Commands.UpdateProductCommand
{
    public record UpdateProductCommand (
        string Id,
        string? Name,
        string Image,
        double Price,
        int Quantity,
        //int CPC,
        string CategoryId,
        string AdId
        ) : IRequest<ProductEntity>;
}
