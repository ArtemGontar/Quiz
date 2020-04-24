using System;
using System.Threading.Tasks;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;
using MongoDB.Driver;

namespace AppQuiz.Persistence
{
    public class QuizRepository : Repository<Quiz>
    {
        public QuizRepository(IMongoCollection<Quiz> mongoCollection) : base(mongoCollection)
        {
        }
        public QuizRepository(QuizDbContext context)
            : base(context)
        {
        }

        public override async Task<bool> SaveAsync(Quiz entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            var update = Update
                .Set(x => x.Title, entity.Title)
                .Set(x => x.OwnerId, entity.OwnerId);
                
            var result = await Collection.UpdateOneAsync(FilterId(entity.Id), update, OptionUpsert);

            return IsUpdated(result);
        }

        public override async Task<bool> DeleteAsync(ISpecification<Quiz> specification)
        {
            var result = await Collection.DeleteOneAsync(specification.Predicate);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
