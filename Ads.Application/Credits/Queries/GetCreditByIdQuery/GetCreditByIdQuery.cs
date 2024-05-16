using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Queries.GetCreditByIdQuery;

public class GetCreditByIdQuery : IRequest<CreditEntity>
{
    public string Id { get; set; }
    public GetCreditByIdQuery(string id)
    {
        Id = id;
    }
}
