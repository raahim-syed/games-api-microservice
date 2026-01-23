using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{
    public interface IItemsRepository
    {
        Task<Item> CreateItemAsync(Item entity);
        Task DeleteItemAsync(Guid id);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetItemByIdAsync(Guid id);
        Task<Item> UpdateItemAsync(Item entity);
    }
}
