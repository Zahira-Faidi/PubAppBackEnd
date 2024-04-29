using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Commands.UpdateCategoryCommand
{
    public record UpdateCategoryCommand(string Id, string Name):IRequest<CategoryEntity>;
}
