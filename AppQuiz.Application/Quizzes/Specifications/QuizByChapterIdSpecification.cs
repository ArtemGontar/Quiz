using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AppQuiz.Domain;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Application.Quizzes.Specifications
{
    public class QuizByChapterIdSpecification : ISpecification<Quiz>
    {
        public Expression<Func<Quiz, bool>> Predicate { get; }

        public QuizByChapterIdSpecification(Guid chapterId)
        {
            Predicate = d => d.ChapterId == chapterId;
        }
    }
}
