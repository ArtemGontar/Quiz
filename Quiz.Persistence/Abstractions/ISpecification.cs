using System;
using System.Linq.Expressions;

namespace AppQuiz.Persistence.Abstractions
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
}
