﻿using System;
using System.Threading.Tasks;
using AppQuiz.Domain;
using MongoDB.Driver;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Persistence
{
    public class QuestionRepository : Repository<Question>
    {
        public QuestionRepository(QuizDbContext context) : base(context)
        {
        }

        public override async Task<bool> SaveAsync(Question entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            var update = Update
                .Set(x => x.CorrectAnswer, entity.CorrectAnswer)
                .Set(x => x.Options, entity.Options)
                .Set(x => x.Title, entity.Title)
                .Set(x => x.QuizId, entity.QuizId);

            var result = await Collection.UpdateOneAsync(FilterId(entity.Id), update, OptionUpsert);

            return IsUpdated(result);
        }

        public override async Task<bool> DeleteAsync(ISpecification<Question> specification)
        {
            var result = await Collection.DeleteOneAsync(specification.Predicate);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
