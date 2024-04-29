using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Queries.GetPromotionByIdQuery
{
    public class GetPromotionByIdQuery : IRequest<PromotionEntity>
    {
        public string Id { get; set; }
        public GetPromotionByIdQuery(string id) 
        { 
            Id = id;
        }
    }
}
