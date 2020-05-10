using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AppQuiz.Domain;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Application.Questions.Specifications
{
    public class QuestionsByQuizIdSpecification : ISpecification<Question>
    {
        public Expression<Func<Question, bool>> Predicate { get; }

        public QuestionsByQuizIdSpecification(Guid quizId)
        {
            Predicate = d => d.QuizId == quizId;
        }
    }
}
