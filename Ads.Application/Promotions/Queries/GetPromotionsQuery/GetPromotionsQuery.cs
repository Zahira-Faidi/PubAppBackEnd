using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Queries.GetPromotionsQuery
{
    public record GetPromotionsQuery : IRequest<List<PromotionEntity>>;

}
