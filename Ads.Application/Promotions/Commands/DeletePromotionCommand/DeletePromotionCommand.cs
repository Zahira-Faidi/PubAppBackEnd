using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Commands.DeletePromotionCommand
{
    public class DeletePromotionCommand : IRequest<PromotionEntity>
    {
        public string Id { get; set; }
        public DeletePromotionCommand(string id)
        {
            Id = id;
        }
    }
}
