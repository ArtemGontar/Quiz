using System;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;
using Microsoft.Extensions.Options;

namespace AppQuiz.Persistence
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        protected override void RegisterClassMaps()
        {
            RegisterClassMap<Quiz, Guid>(d => d.Id);
            RegisterClassMap<Question, Guid>(d => d.Id);
        }
    }
}
