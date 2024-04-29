using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAdsQuery
{
    public record GetAdsQuery : IRequest<List<AdEntity>>;
}
