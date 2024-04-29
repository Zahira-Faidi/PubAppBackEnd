using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdQuery : IRequest<ProductEntity>
    {
        public string Id { get; set; }
        public GetProductByIdQuery(string id)
        {
            Id = id;
        }
    }
}
