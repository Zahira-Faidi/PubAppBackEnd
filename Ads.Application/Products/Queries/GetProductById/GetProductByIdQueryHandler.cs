using Ads.Application.Common.Interfaces;
using Ads.Domain.Common.Errors;
using Ads.Domain.Entities;
using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            if (string.IsNullOrEmpty(request.Id))
            {
                return Errors.Global.IdNotFound;
            }

            var product = await _repository.GetDetailsAsync(request.Id, cancellationToken);

            if (product == null)
            {
                return Errors.Global.IdNotFound;
            }

            return product;
        }
    }
}