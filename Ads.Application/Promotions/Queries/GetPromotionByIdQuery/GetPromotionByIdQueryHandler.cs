using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Queries.GetPromotionByIdQuery
{
    public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, PromotionEntity>
    {
        private readonly ICommonRepository<PromotionEntity> _repository;

        public GetPromotionByIdQueryHandler(ICommonRepository<PromotionEntity> promotionRepository)
        {
            _repository = promotionRepository;
        }

        public async Task<PromotionEntity> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetDetailsAsync(request.Id, cancellationToken);
        }
    }
}
