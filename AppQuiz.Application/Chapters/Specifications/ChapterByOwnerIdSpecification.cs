using AppQuiz.Domain;
using Shared.Persistence.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AppQuiz.Application.Chapters.Specifications
{
    public class ChapterByOwnerIdSpecification : ISpecification<Chapter>
    {
        public Expression<Func<Chapter, bool>> Predicate { get; }
        public ChapterByOwnerIdSpecification(Guid ownerId)
        {
            Predicate = d => d.OwnerId == ownerId;
        }
    }
}
