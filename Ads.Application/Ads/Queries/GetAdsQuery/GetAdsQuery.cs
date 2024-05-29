using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAdsQuery
{
    public class GetAdsQuery : IRequest<List<AdEntity>>
    {
        public bool IsDeleted { get; set; }
    }
}
