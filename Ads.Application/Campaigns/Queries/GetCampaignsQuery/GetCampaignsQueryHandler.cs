using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Queries.GetCampaignsQuery
{
    public class GetCampaignsQueryHandler : IRequestHandler<GetCampaignsQuery, List<CampaignEntity>>
    {
        private readonly ICommonRepository<CampaignEntity> _repository;
        public GetCampaignsQueryHandler(ICommonRepository<CampaignEntity> repository)
        {
            _repository = repository;
        }

        public async Task<List<CampaignEntity>> Handle(GetCampaignsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
