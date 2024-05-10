using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Application.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryEntity>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository entity, IMapper mapper)
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
