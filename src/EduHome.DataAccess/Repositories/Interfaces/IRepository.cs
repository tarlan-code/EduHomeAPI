using EduHome.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EduHome.DataAccess.Repositories.Interfaces;
public interface IRepository<T> where T : class,IBaseEntity,new()
{
    IQueryable<T> GetAll();
    IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression, bool isTracking = false);
    Task<T?> FindByIdAsync(Guid id);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveAsync();

    DbSet<T> Table { get;}
}
