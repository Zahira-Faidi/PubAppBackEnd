using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Queries.GetCategoriesQuery
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryEntity>>
    {
        private readonly ICommonRepository<CategoryEntity> _repository;
        public GetCategoriesQueryHandler(ICommonRepository<CategoryEntity> repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryEntity>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
