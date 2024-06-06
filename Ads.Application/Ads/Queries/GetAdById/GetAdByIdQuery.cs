using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAdById
{
    public class GetAdByIdQuery : IRequest<AdEntity>
    {
        public string Id { get; set; }
        public GetAdByIdQuery(string id)
        {
            Id = id;
        }
    }
}
