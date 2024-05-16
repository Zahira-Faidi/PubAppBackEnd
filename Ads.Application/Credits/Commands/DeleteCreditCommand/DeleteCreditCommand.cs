using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Commands.DeleteCreditCommand;

public class DeleteCreditCommand : IRequest<CreditEntity>
{
    public string Id { get; set; }
    public DeleteCreditCommand(string id)
    {
        Id = id;
    }
}
