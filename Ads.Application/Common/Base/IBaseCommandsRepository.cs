using Ads.Domain.Common.Entities;

namespace Ads.Application.Common.Base;

public interface IBaseCommandsRepository<T> where T : BaseEntity
{
    Task<T> InsertAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(string id, T entity, CancellationToken cancellationToken);
    Task<T> DeleteAsync(string id, CancellationToken cancellationToken);
}
