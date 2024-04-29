using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Queries.GetPromotionsQuery
{
    public class GetPromotionQueryHandler : IRequestHandler<GetPromotionsQuery, List<PromotionEntity>>
    {
        private readonly ICommonRepository<PromotionEntity> _repository;
        public GetPromotionQueryHandler(ICommonRepository<PromotionEntity> repository) 
        { 
            _repository = repository;
        }
        public async Task<List<PromotionEntity>> Handle(GetPromotionsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
