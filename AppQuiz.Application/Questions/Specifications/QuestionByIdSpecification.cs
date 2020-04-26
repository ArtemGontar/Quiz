using AppQuiz.Domain;
using Shared.Persistence.MongoDb;
using System;
using System.Linq.Expressions;

namespace AppQuiz.Application.Questions.Specifications
{
    public class QuestionByIdSpecification : ISpecification<Question>
    {
        public Expression<Func<Question, bool>> Predicate { get; }
        public QuestionByIdSpecification(Guid questionId)
        {
            Predicate = d => d.Id == questionId;
        }
    }
}
