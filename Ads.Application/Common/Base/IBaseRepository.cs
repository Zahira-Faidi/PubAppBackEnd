using Ads.Domain.Common.Entities;

namespace Ads.Application.Common.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetDetailsAsync(string id, CancellationToken cancellationToken);
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(string id, T entity, CancellationToken cancellationToken);
        Task<T> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
