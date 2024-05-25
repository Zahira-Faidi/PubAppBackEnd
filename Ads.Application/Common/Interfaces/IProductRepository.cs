using Ads.Application.Common.Base;
using Ads.Domain.Entities;

namespace Ads.Application.Common.Interfaces
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    { 
        Task<List<ProductEntity>> GetAllProductsByAdId(string adId, CancellationToken cancellationToken);
    }
}
