// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MongoDB.Driver;
// using Play.Catalog.Service.Entities;

// namespace Play.Catalog.Service.Repositories
// {

//     public class ItemsRepository : IItemsRepository
//     {
//         private const string collectionName = "items";

//         // MongoDB collection reference (DB Table)
//         private readonly IMongoCollection<Item> dbCollection;

//         // Filter definition builder to help create filters
//         private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

//         public ItemsRepository(IMongoDatabase database)
//         {
//             // Initialize Client and get database and collection
//             // var MongoClient = new MongoClient("mongodb://localhost:27017");

//             // var database = MongoClient.GetDatabase("Catalog");

//             dbCollection = database.GetCollection<Item>(collectionName);
//         }

//         // Methods for CRUD operations ------------------------------------------

//         public async Task<IReadOnlyCollection<Item>> GetAllAsync()
//         {
//             return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
//         }

//         public async Task<Item> GetItemByIdAsync(Guid id)
//         {
//             var filter = filterBuilder.Eq(item => item.Id, id);
//             return await dbCollection.Find(filter).SingleOrDefaultAsync();
//         }

//         public async Task<Item> CreateItemAsync(Item entity)
//         {
//             if (entity == null)
//             {
//                 throw new ArgumentNullException(nameof(entity));
//             }

//             await dbCollection.InsertOneAsync(entity);

//             return entity;
//         }


//         public async Task<Item> UpdateItemAsync(Item entity)
//         {
//             if (entity == null)
//             {
//                 throw new ArgumentNullException(nameof(entity));
//             }

//             var filter = filterBuilder.Eq(existingItem => existingItem.Id, entity.Id);
//             await dbCollection.ReplaceOneAsync(filter, entity);

//             return entity;
//         }

//         public async Task DeleteItemAsync(Guid id)
//         {
//             var filter = filterBuilder.Eq(item => item.Id, id);
//             await dbCollection.DeleteOneAsync(filter);
//         }
//     }
// }
