using MyMovieDB.Models;

namespace MyMovieDB.Data;

public interface IRepository<T> where T : BaseModel
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(int id);
    Task AddAsync(T entity);
    T Update(T entity);
    Task<T?> DeleteAsync(int id);
}