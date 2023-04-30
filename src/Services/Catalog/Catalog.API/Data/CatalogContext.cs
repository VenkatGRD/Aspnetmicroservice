using Catalog.API.Enities;
using MongoDB.Driver;
using System;
using Microsoft.Extensions.Configuration;
namespace Catalog.API.Data
{
    public class CatalogContext:ICatalogContext
    {
      public CatalogContext(IConfiguration configuration) {
        var client=new MongoClient(configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
             Products = database.GetCollection<Product>(configuration.GetValue<string>("DataBaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        //public IMongoCollection<Product> Products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<Product> Products { get; }
    }
}
