namespace HotelListing.API.Data.Contracts;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(int? id);

    Task<IEnumerable<T>> GetAllAsync();

    Task DeleteAsync(int id);

    Task<T> CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task<bool> Exists(int id);
}