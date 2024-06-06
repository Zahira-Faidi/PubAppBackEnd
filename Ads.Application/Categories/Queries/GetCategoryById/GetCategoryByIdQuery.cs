using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Categories.Queries.GetCategoryByIdQuery
{
    public class GetCategoryByIdQuery : IRequest<CategoryEntity>
    {
        public string Id { get; set; }
        public GetCategoryByIdQuery(string id) 
        {
            Id = id;
        }
    }
}
