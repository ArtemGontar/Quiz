using System;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AppQuiz.Persistence.Abstractions
{
    public sealed class ConnectionStrings
    {
        public const string SECTION_NAME = "ConnectionStrings";

        public string Mongo { get; set; }
    }
    public abstract class DbContext
    {
        protected readonly IMongoDatabase Database;

        protected DbContext(IOptions<ConnectionStrings> connectionStrings)
        {
            RegisterClassMaps();

            var mongodbUrl = new MongoUrl(connectionStrings.Value.Mongo);
            var client = new MongoClient(mongodbUrl);
            Database = client.GetDatabase(mongodbUrl.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>() => Database.GetCollection<T>(typeof(T).Name);
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return Database.GetCollection<T>(name);
        }
        public IMongoCollection<BsonDocument> GetCollection(string name) => Database.GetCollection<BsonDocument>(name);

        protected abstract void RegisterClassMaps();

        protected void RegisterClassMap<T>(Action<BsonClassMap<T>> classMapInitializer)
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T)))
                return;

            BsonClassMap.RegisterClassMap(classMapInitializer);
        }

        protected void RegisterClassMap<T>()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T)))
                return;

            BsonClassMap.RegisterClassMap<T>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        protected void RegisterClassMap<T>(BsonClassMap<T> classMap)
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T)))
                return;

            BsonClassMap.RegisterClassMap(classMap);
        }

        protected void RegisterClassMap<T, TMember>(Expression<Func<T, TMember>> idField)
        {
            RegisterClassMap<T>(x =>
            {
                x.AutoMap();
                x.MapIdField(idField);
                x.SetIgnoreExtraElements(true);
            });
        }
    }
}
