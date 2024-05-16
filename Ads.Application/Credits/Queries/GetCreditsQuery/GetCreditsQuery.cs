using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Queries.GetCreditsQuery
{
    public class GetCreditsQuery : IRequest<List<CreditEntity>>
    {
    }
}
