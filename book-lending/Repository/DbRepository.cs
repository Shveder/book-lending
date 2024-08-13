using System.Linq.Expressions;
using book_lending.Data;
using book_lending.Models.Interfaces;
using book_lending.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace book_lending.Repository;

public class DbRepository : IDbRepository
{
    private readonly DataContext _context;

    public DbRepository(DataContext context)
    {
        _context = context;
    }

    public IQueryable<T> Get<T>() where T : class, IModels
    {
        return _context.Set<T>().AsQueryable();
    }

    public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IModels
    {
        return _context.Set<T>().Where(selector).AsQueryable();
    }

    public async Task<Guid> Add<T>(T newEntity) where T : class, IModels
    {
        var entity = await _context.Set<T>().AddAsync(newEntity);
        Console.WriteLine(entity.Entity.Id);
        return entity.Entity.Id;
    }


    public async Task Delete<T>(Guid id) where T : class, IModels
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Update<T>(T entity) where T : class, IModels
    {
        await Task.Run(() => _context.Set<T>().Update(entity));
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public IQueryable<T> GetAll<T>() where T : class, IModels
    {
        return _context.Set<T>().AsQueryable();
    }
}