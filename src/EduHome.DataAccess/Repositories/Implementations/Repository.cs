using EduHome.Core.Entities.Common;
using EduHome.DataAccess.Contexts;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EduHome.DataAccess.Repositories.Implementations;
public class Repository<T> : IRepository<T> where T : class, IBaseEntity, new()
{
    readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table { get => _context.Set<T>(); }
    public IQueryable<T> GetAll() => Table.AsQueryable().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false)
    => isTracking ? Table.Where(expression) : Table.Where(expression).AsNoTracking();

    public async Task<T?> FindByIdAsync(Guid id)
   => await Table.FindAsync(id);


    
   
    
    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }
    public void Update(T entity)
    {
        Table.Update(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
