using AppQuiz.Domain;
using MongoDB.Driver;
using Shared.Persistence.MongoDb;
using System;
using System.Threading.Tasks;

namespace AppQuiz.Persistence
{
    public class ChapterRepository : Repository<Chapter>
    {
        public ChapterRepository(QuizDbContext context) : base(context)
        {
            
        }

        public override async Task<bool> SaveAsync(Chapter entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            var update = Update
                .Set(x => x.Name, entity.Name)
                .Set(x => x.EnglishLevel, entity.EnglishLevel)
                .Set(x => x.OwnerId, entity.OwnerId);

            var result = await Collection.UpdateOneAsync(FilterId(entity.Id), update, OptionUpsert);

            return IsUpdated(result);
        }

        public override async Task<bool> DeleteAsync(ISpecification<Chapter> specification)
        {
            var result = await Collection.DeleteOneAsync(specification.Predicate);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
