namespace PrismMauiApp.Services
{
    public interface IGenericRepository<T>
    {
        Task<bool> AddAsync(T item);

        Task<bool> UpdateAsync(T item);

        Task<bool> DeleteAsync(string id);

        Task<T> GetById(string id);

        Task<IEnumerable<T>> GetAsync(bool forceRefresh = false);
    }
}