using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryEntity>
    {
        private readonly ICommonRepository<CategoryEntity> _repository;
        public DeleteCategoryCommandHandler(ICommonRepository<CategoryEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CategoryEntity>  Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (categoryToDelete == null)
                throw new Exception($"Category with ID {request.Id} not found.");
            else
                await _repository.DeleteAsync(request.Id, cancellationToken);
            return categoryToDelete;
        }
    }
}
