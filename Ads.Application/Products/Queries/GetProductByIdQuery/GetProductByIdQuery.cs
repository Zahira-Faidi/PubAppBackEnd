using Ads.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Ads.Application.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdQuery : IRequest<ErrorOr<ProductEntity>>
    {
        public string Id { get; set; }
        public GetProductByIdQuery(string id)
        {
            Id = id;
        }
    }
}
