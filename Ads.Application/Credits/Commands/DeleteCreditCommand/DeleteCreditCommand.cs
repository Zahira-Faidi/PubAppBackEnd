using MediatR;

namespace Ads.Application.Credits.Commands.DeleteCreditCommand;

public class DeleteCreditCommand : IRequest<Unit>
{
    public string Id { get; set; }
    public DeleteCreditCommand(string id)
    {
        Id = id;
    }
}
