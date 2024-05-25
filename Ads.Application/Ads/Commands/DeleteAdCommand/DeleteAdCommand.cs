using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.DeleteAdCommand
{
    public class DeleteAdCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public DeleteAdCommand(string id)
        {
            Id = id;
        }
    }
}
