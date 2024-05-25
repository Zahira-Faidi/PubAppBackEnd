using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Commands.DeleteBudgetCommand
{
    public class DeleteBudgetCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public DeleteBudgetCommand(string id) 
        {
            Id = id;
        }
    }
}
