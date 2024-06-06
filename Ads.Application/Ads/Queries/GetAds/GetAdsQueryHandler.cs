using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAds
{
    public class GetAdsQueryHandler : IRequestHandler<GetAdsQuery, List<AdEntity>>
    {
        private readonly IAdRepository _repository;
        public GetAdsQueryHandler(IAdRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<AdEntity>> Handle(GetAdsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
