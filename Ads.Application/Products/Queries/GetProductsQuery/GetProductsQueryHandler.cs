using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Queries.GetProductsQuery
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductEntity>>
    {
        private readonly ICommonRepository<ProductEntity> _repository;
        public GetProductsQueryHandler(ICommonRepository<ProductEntity> productRepository)
        {
            _repository = productRepository;
        }

        public async Task<List<ProductEntity>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
