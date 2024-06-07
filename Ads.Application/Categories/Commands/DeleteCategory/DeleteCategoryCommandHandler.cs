using Ads.Application.Common.Interfaces;
using MediatR;

namespace Ads.Application.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;
        public DeleteCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit>  Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (categoryToDelete == null)
                throw new InvalidOperationException($"Category with ID {request.Id} not found.");
            else
                await _repository.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
