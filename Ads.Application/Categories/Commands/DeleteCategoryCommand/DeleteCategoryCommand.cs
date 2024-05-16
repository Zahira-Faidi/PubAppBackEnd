using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Commands.DeleteCategoryCommand;
public class DeleteCategoryCommand : IRequest<CategoryEntity>
{
    public string Id { get; set; }
    public DeleteCategoryCommand(string id)
    {
        Id = id;
    }
}