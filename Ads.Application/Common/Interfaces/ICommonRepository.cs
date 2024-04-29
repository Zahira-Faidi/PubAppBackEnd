namespace Ads.Application.Common.Interfaces
{
    public interface ICommonRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetDetailsAsync(string id, CancellationToken cancellationToken);
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<T> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
