using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Commands.UpdatePromotionCommand
{
    public record UpdatePromotionCommand
    (
        string Id,
        string Description,
        double Discount,
        List<string> Products
    ) : IRequest<PromotionEntity>;
}
