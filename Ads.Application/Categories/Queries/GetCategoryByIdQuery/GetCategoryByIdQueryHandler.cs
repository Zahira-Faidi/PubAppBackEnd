using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Queries.GetCategoryByIdQuery
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryEntity>
    {
        public readonly ICommonRepository<CategoryEntity> _repository;
        public GetCategoryByIdQueryHandler(ICommonRepository<CategoryEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CategoryEntity> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetDetailsAsync(request.Id, cancellationToken) ;
        }
    }
}
