using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Commands.CreatePromotionCommand
{
    public record CreatePromotionCommand(
        string Description,
        double Discount,
        List<string> Products
        ):IRequest<PromotionEntity>;
}
