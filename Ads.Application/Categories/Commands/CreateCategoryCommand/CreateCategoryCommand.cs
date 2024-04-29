using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Commands.CreateCategoryCommand
{
    public record CreateCategoryCommand(string Name) : IRequest<CategoryEntity>;
}
