using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Queries.GetProductsQuery
{
    public record GetProductsQuery : IRequest<List<ProductEntity>>;
}
