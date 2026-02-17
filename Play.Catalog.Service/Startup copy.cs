// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.OpenApi.Models;
// using MongoDB.Bson;
// using MongoDB.Bson.Serialization;
// using MongoDB.Bson.Serialization.Serializers;
// using MongoDB.Driver;
// using Play.Catalog.Service.Entities;
// using Play.Catalog.Service.Repositories;
// using Play.Catalog.Service.Settings;

// namespace Play.Catalog.Service
// {
//     public class Startup
//     {
//         private ServiceSettings serviceSettings;
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }

//         // This method gets called by the runtime. Use this method to add services to the container.
//         public void ConfigureServices(IServiceCollection services)
//         {

//             // Convert Incoming Guid and DateTimeOffset to string for storing in MongoDB
//             BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
//             BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

//             // Get settings to construct our client
//             serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            
            
//             // Adding MongoDB client as a Singleton since it is thread-safe and can be shared across the application
//             services.AddSingleton(serviceProvider =>
//             {
//                 var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

//                 var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

//                 return mongoClient.GetDatabase(serviceSettings.ServiceName);
//             });

//             // Adding Repository as a Singleton since it doesn't have any state and can be shared across the application
//             // services.AddSingleton<IItemsRepository, ItemsRepository>();
//             services.AddSingleton<IRepository<Item>>(serviceProvider =>
//             {
//                 var database = serviceProvider.GetService<IMongoDatabase>();
//                 return new MongoRepository<Item>(database, "items");
//             });

//             services.AddControllers(options =>
//             {
//                 // By default, ASP.NET Core adds "Async" suffix to action names that return Task or Task<T>. 
//                 // This can be confusing for clients consuming the API. 
//                 // Setting this option to false will prevent the framework from adding "Async" suffix to action names.
//                 options.SuppressAsyncSuffixInActionNames = false;
//             });  
//             services.AddSwaggerGen(c =>
//             {
//                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Play.Catalog.Service", Version = "v1" });
//             });
//         }

//         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//                 app.UseSwagger();
//                 app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Play.Catalog.Service v1"));
//             }

//             app.UseHttpsRedirection();

//             app.UseRouting();

//             app.UseAuthorization();

//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers();
//             });
//         }
//     }
// }
