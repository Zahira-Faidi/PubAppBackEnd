using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Queries.GetCreditsQuery;

public class GetCreditsQueryHandler : IRequestHandler<GetCreditsQuery, List<CreditEntity>>
{
    private readonly ICreditRepository _repository;

    public GetCreditsQueryHandler(ICreditRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CreditEntity>> Handle(GetCreditsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
