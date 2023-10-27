using System.Linq.Expressions;
using Domain.entities;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void RemoveRange(IEnumerable<T> entity);
    void Remove(T entity);
    void Update(T entity);
}
