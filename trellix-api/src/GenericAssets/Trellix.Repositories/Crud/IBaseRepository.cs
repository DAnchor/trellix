namespace Trellix.Repositories.Crud;

public interface IBaseRepository<T>
{
    Task UpdateAsync(T item);

    Task AddAsync(T item);

    Task RemoveAsync(T item);

    Task<IEnumerable<T>> FindAllAsync();
}