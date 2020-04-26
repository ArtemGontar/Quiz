using AppQuiz.Domain;
using Shared.Persistence.MongoDb;
using System;
using System.Linq.Expressions;

namespace AppQuiz.Application.Quizzes.Specifications
{
    public class QuizByIdSpecification : ISpecification<Quiz>
    {
        public Expression<Func<Quiz, bool>> Predicate { get; }

        public QuizByIdSpecification(Guid quizId)
        {
            Predicate = d => d.Id == quizId;
        }
    }
}
