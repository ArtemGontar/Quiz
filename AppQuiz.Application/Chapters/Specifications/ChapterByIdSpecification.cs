using AppQuiz.Domain;
using Shared.Persistence.MongoDb;
using System;
using System.Linq.Expressions;

namespace AppQuiz.Application.Chapters.Specifications
{
    public class ChapterByIdSpecification : ISpecification<Chapter>
    {
        public Expression<Func<Chapter, bool>> Predicate { get; }
        public ChapterByIdSpecification(Guid chapterId)
        {
            Predicate = d => d.Id == chapterId;
        }
    }
}
