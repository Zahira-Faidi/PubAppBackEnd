using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAdsByCampaignIdQuery;

public class GetAdsByCampaignIdQueryHandler : IRequestHandler<GetAdsByCampaignIdQuery, List<AdEntity>>
{
    private readonly IAdRepository _adRepository;
    public GetAdsByCampaignIdQueryHandler(IAdRepository adRepository)
    {
        _adRepository = adRepository;
    }
    public async Task<List<AdEntity>> Handle(GetAdsByCampaignIdQuery request, CancellationToken cancellationToken)
    {
        return await _adRepository.GetAllAdsByCampaignId(request.CampaignId, cancellationToken);
    }
}
