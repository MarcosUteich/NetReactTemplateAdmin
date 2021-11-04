using Domain.Data.ContextRepository;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Data.Context
{
    public class DataBaseContext : IDatabaseContext
    {
        private string _connectionString;
        protected IMongoDatabase _database;

        public DataBaseContext(IMongoClient mongoClient, IConfiguration configuration)
        {
            _database = mongoClient.GetDatabase(configuration["Database:Name"]);
        }

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    TryReadMongoDatabase();

                return _connectionString;
            }
        }

        public IMongoDatabase Database()
        {
            return _database;
        }

        public IQueryable<T> Table<T>(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException(nameof(collectionName));

            return _database.GetCollection<T>(collectionName).AsQueryable();
        }

        protected IMongoDatabase TryReadMongoDatabase()
        {
            var mongourl = new MongoUrl(_connectionString);
            var databaseName = mongourl.DatabaseName;
            var mongodb = new MongoClient(_connectionString).GetDatabase(databaseName);
            return mongodb;
        }

        public async Task CreateTable(string name, string collation)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var database = _database ?? TryReadMongoDatabase();

            if (! await CollectionExists(name))
            {
                if (!string.IsNullOrEmpty(collation))
                {
                    var options = new CreateCollectionOptions();
                    options.Collation = new Collation(collation);
                    await database.CreateCollectionAsync(name, options);
                }
                else
                    await database.CreateCollectionAsync(name);
            }
        }

        public async Task<bool> CollectionExists(string name)
        {
            var database = _database ?? TryReadMongoDatabase();

            var filter = new BsonDocument("name", name);
            var found = await database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });

            return found.Any();
        }

        public async Task DeleteTable(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var database = _database ?? TryReadMongoDatabase();

            await database.DropCollectionAsync(name);
        }

        public async Task CreateIndex<T>(IRepository<T> repository, OrderBuilder<T> orderBuilder, string indexName, bool unique = false) where T : BaseEntity
        {
            if (string.IsNullOrEmpty(indexName))
                throw new ArgumentNullException(nameof(indexName));

            IList<IndexKeysDefinition<T>> keys = new List<IndexKeysDefinition<T>>();
            foreach (var item in orderBuilder.Fields)
            {
                if (item.selector != null)
                {
                    if (item.value)
                    {
                        keys.Add(Builders<T>.IndexKeys.Ascending(item.selector));
                    }
                    else
                    {
                        keys.Add(Builders<T>.IndexKeys.Descending(item.selector));
                    }
                }
                else
                {
                    if (item.value)
                    {
                        keys.Add(Builders<T>.IndexKeys.Ascending(item.fieldName));
                    }
                    else
                    {
                        keys.Add(Builders<T>.IndexKeys.Descending(item.fieldName));
                    }
                }
            }

            try
            {
                await ((Repository<T>)repository).Collection.Indexes.CreateOneAsync(new CreateIndexModel<T>(Builders<T>.IndexKeys.Combine(keys),
                    new CreateIndexOptions() { Name = indexName, Unique = unique }));
            }
            catch { }
        }

        public async Task DeleteIndex<T>(IRepository<T> repository, string indexName) where T : BaseEntity
        {
            if (string.IsNullOrEmpty(indexName))
                throw new ArgumentNullException(nameof(indexName));
            try
            {
                await ((Repository<T>)repository).Collection.Indexes.DropOneAsync(indexName);
            }
            catch { }
        }
    }
}
