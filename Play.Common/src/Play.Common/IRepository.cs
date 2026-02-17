using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Play.Common;

namespace Play.Common
{
    // This is a generic repository interface that defines the basic CRUD operations for any entity that implements the IEntity interface.
    public interface IRepository<T> where T : IEntity
    {
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(Guid id); 
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter);
        Task<T> UpdateAsync(T entity);
    }
}
