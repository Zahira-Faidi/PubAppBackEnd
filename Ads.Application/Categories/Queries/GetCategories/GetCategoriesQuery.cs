using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Queries.GetCategoriesQuery
{
    public record GetCategoriesQuery : IRequest<List<CategoryEntity>>;
}
