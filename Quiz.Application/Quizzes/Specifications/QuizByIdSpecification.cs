using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;

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
