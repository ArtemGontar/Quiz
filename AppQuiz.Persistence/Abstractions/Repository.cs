using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AppQuiz.Persistence.Abstractions
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected virtual IMongoCollection<T> Collection { get; }

        protected FilterDefinitionBuilder<T> Filter => Builders<T>.Filter;
        protected FilterDefinition<T> FilterEmpty => FilterDefinition<T>.Empty;
        protected UpdateDefinitionBuilder<T> Update => Builders<T>.Update;
        protected ProjectionDefinitionBuilder<T> Project => Builders<T>.Projection;

        protected UpdateOptions OptionUpsert => new UpdateOptions { IsUpsert = true };

        protected Repository(IMongoCollection<T> mongoCollection)
        {
            Collection = mongoCollection;
        }
        protected Repository(DbContext context)
        {
            Collection = context.GetCollection<T>();
        }

        public Task<long> CountAsync()
            => Collection.CountDocumentsAsync(FilterEmpty);
        public Task<long> CountAsync(ISpecification<T> specification)
            => Collection.CountDocumentsAsync(specification.Predicate);

        public Task<bool> AnyAsync()
            => Collection.Find(FilterEmpty).AnyAsync();
        public Task<bool> AnyAsync(ISpecification<T> specification)
            => Collection.Find(specification.Predicate).AnyAsync();

        public Task<T> GetAsync(ISpecification<T> specification)
            => Collection.Find(specification.Predicate).FirstOrDefaultAsync();
        
        public Task<List<T>> GetAllAsync()
            => Collection.Find(FilterEmpty).ToListAsync();

        public Task<List<T>> GetAllAsync(ISpecification<T> specification)
            => Collection.Find(specification.Predicate).ToListAsync();

        //public Task<List<T>> GetAllAsync(ISpecification<T> specification, ISorting<T> sorting)
        //{
        //    var query = Collection.Find(specification.Predicate);

        //    return sorting.SortingType == SortingType.Ascending ?
        //        query.SortBy(sorting.Selector).ToListAsync() :
        //        query.SortByDescending(sorting.Selector).ToListAsync();
        //}

        public IEnumerable<T> GetAll()
            => Collection.Find(FilterEmpty).ToEnumerable();
        public IEnumerable<T> GetAll(ISpecification<T> specification)
            => Collection.Find(specification.Predicate).ToEnumerable();

        public abstract Task<bool> SaveAsync(T entity);

        public virtual async Task<bool> DeleteAsync(ISpecification<T> specification)
        {
            var result = await Collection.DeleteOneAsync(specification.Predicate).ConfigureAwait(false);
            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public virtual async Task<bool> DeleteManyAsync(ISpecification<T> specification)
        {
            var result = await Collection.DeleteManyAsync(specification.Predicate);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public virtual Task<bool> SaveBulkAsync(IEnumerable<T> entities) => throw new NotImplementedException();

        protected static FilterDefinition<T> FilterId(object key) => Builders<T>.Filter.Eq("Id", key);

        protected bool IsUpdated(UpdateResult result) => result.IsAcknowledged && (result.UpsertedId != null || result.ModifiedCount == 1 || result.MatchedCount == 1);
        protected bool IsUpdated(ReplaceOneResult result) => result.IsAcknowledged && (result.UpsertedId != null || result.ModifiedCount == 1 || result.MatchedCount == 1);
    }
}
