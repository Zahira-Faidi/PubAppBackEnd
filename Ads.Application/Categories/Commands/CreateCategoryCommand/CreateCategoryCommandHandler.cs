using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Application.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryEntity>
    {
        private readonly ICommonRepository<CategoryEntity> _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICommonRepository<CategoryEntity> entity, IMapper mapper)
        {
            _repository = entity;
            _mapper = mapper;
        }

        public async Task<CategoryEntity> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryEntity>(request);
            var result = await _repository.InsertAsync(category, cancellationToken);
            return result;
        }
    }
}
