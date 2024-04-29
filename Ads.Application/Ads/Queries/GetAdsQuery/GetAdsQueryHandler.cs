using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAdsQuery
{
    public class GetAdsQueryHandler : IRequestHandler<GetAdsQuery, List<AdEntity>>
    {
        private readonly ICommonRepository<AdEntity> _repository;
        public GetAdsQueryHandler(ICommonRepository<AdEntity> repository)
        {
            _repository = repository;
        }
        public async Task<List<AdEntity>> Handle(GetAdsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
