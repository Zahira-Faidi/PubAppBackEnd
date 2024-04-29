using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryEntity>
    {
        private readonly ICommonRepository<CategoryEntity> _repository;
        public UpdateCategoryCommandHandler(ICommonRepository<CategoryEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CategoryEntity> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCategory = await _repository.GetDetailsAsync(request.Id, cancellationToken);

                if (existingCategory == null)
                {
                    throw new Exception($"Category with id {request.Id} not found");
                }

                existingCategory.Name = request.Name;

                await _repository.UpdateAsync(existingCategory, cancellationToken);

                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update Category: {ex.Message}", ex);
            }
        }
    }
}
