using Ads.Application.Common.Interfaces;
using Ads.Domain.Common.Errors;
using Ads.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Ads.Application.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductEntity>>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public async Task<ErrorOr<ProductEntity>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            if (id == null) return Errors.Global.IdNotFound;
            return await _repository.GetDetailsAsync(id, cancellationToken);
        }
    }
}