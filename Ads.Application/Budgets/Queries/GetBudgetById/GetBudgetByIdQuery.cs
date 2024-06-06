using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Queries.GetBudgetById
{
    public class GetBudgetByIdQuery : IRequest<BudgetEntity>
    {
        public string Id { get; set; }
        public GetBudgetByIdQuery(string id) 
        {
            Id = id;
        }
    }
}
