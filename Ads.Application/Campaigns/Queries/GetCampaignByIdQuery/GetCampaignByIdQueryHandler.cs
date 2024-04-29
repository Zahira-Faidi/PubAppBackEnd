using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Queries.GetCampaignByIdQuery
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, CampaignEntity>
    {
        private readonly ICommonRepository<CampaignEntity> _repository;

        public GetCampaignByIdQueryHandler(ICommonRepository<CampaignEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CampaignEntity> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetDetailsAsync(request.Id, cancellationToken);
        }
    }
}
