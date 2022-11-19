using Microsoft.EntityFrameworkCore;
using MyMovieDB.Models;

namespace MyMovieDB.Data;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await SaveChangesAsync();
    }

    public virtual async Task<T?> DeleteAsync(int id)
    {
        T? entity = await GetAsync(id);

        if (entity is not null)
        {
            _context.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        return entity;
    }

    public virtual async Task<T?> GetAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> UpdateAsync(T entity)
    {
        T? entityInDB = await GetAsync(entity.Id);
        _context.ChangeTracker.Clear();

        if (entityInDB is not null)
        {
            T updatedEntity = _context.Set<T>().Update(entity).Entity;
            await SaveChangesAsync();

            return updatedEntity;
        }

        return null;
    }

    protected async Task SaveChangesAsync()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            var entity = entry.Entity as BaseModel;

            if (entity is null)
            {
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity.ModifiedAt = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entity.DeletedAt = DateTime.UtcNow;
                    entry.State = EntityState.Modified;
                    break;
            }
        };

        await _context.SaveChangesAsync();
    }
}