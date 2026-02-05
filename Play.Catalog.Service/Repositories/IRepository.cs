using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{
    // This is a generic repository interface that defines the basic CRUD operations for any entity that implements the IEntity interface.
    public interface IRepository<T> where T : IEntity
    {
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> UpdateAsync(T entity);
    }
}
