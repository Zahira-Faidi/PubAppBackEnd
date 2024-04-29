using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAdByIdQuery
{
    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, AdEntity>
    {
        private readonly ICommonRepository<AdEntity> _repository;
        public GetAdByIdQueryHandler(ICommonRepository<AdEntity> repository)
        {
            _repository = repository;
        }

        public async Task<AdEntity> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetDetailsAsync(request.Id, cancellationToken);
        }
    }
}
