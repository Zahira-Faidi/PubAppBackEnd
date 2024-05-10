using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAdByIdQuery
{
    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, AdEntity>
    {
        private readonly IAdRepository _repository;
        public GetAdByIdQueryHandler(IAdRepository repository)
        {
            _repository = repository;
        }

        public async Task<AdEntity> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetDetailsAsync(request.Id, cancellationToken);
        }
    }
}
