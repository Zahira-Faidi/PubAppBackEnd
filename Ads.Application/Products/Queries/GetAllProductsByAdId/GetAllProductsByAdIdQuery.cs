using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Queries.GetAllProductsByAdIdQuery;

public class GetAllProductsByAdIdQuery : IRequest<List<ProductEntity>>
{
    public string AdId { get; set; }
    public GetAllProductsByAdIdQuery(string adId)
    {
        AdId = adId;
    }
}
