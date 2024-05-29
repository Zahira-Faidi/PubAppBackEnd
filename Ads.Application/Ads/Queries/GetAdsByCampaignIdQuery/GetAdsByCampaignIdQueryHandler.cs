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
        var oldList = await _adRepository.GetAllAdsByCampaignId(request.CampaignId, cancellationToken);
        List<AdEntity> ads = new List<AdEntity>();
        foreach (var old in oldList)
        {
            if(old.IsDeleted == false)
            {
                ads.Add(old);
            }
        }
        return ads;
    }
}
