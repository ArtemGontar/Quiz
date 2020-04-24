using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;

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
