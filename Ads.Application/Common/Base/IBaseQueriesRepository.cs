using Ads.Domain.Common.Entities;

namespace Ads.Application.Common.Base;

public interface IBaseQueriesRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> GetDetailsAsync(string id, CancellationToken cancellationToken);
}
