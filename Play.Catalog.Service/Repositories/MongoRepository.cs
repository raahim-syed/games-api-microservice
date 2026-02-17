using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{

    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {

        // MongoDB collection reference (DB Table)
        private readonly IMongoCollection<T> dbCollection;

        // Filter definition builder to help create filters
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            // Initialize Client and get database and collection
            // var MongoClient = new MongoClient("mongodb://localhost:27017");

            // var database = MongoClient.GetDatabase("Catalog");

            dbCollection = database.GetCollection<T>(collectionName);
        }

        // Methods for CRUD operations ------------------------------------------

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await dbCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollection.InsertOneAsync(entity);

            return entity;
        }


        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var filter = filterBuilder.Eq(existingItem => existingItem.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
