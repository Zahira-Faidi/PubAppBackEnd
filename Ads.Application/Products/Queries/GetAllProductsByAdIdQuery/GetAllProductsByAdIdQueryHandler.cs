using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Queries.GetAllProductsByAdIdQuery;

public class GetAllProductsByAdIdQueryHandler : IRequestHandler<GetAllProductsByAdIdQuery, List<ProductEntity>>
{
    private readonly IProductRepository _productRepository;
    public GetAllProductsByAdIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductEntity>> Handle(GetAllProductsByAdIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllProductsByAdId(request.AdId, cancellationToken);
    }
}
