using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAds
{
    public class GetAdsQuery : IRequest<List<AdEntity>>
    {
        public bool IsDeleted { get; set; }
    }
}
