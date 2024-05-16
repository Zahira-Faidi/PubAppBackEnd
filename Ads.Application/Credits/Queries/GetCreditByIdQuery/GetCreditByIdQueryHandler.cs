using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Queries.GetCreditByIdQuery;

public class GetCreditByIdQueryHandler : IRequestHandler<GetCreditByIdQuery, CreditEntity>
{
    private ICreditRepository _repositoy;
    public GetCreditByIdQueryHandler(ICreditRepository repositoy)
    {
        _repositoy = repositoy;
    }

    public async Task<CreditEntity> Handle(GetCreditByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        return await _repositoy.GetDetailsAsync(id, cancellationToken);
    }
}
