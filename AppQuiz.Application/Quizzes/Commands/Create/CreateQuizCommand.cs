using System;
using MediatR;
using Shared.Common;

namespace AppQuiz.Application.Quizzes.Commands.Create
{
    public class CreateQuizCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public Guid ChapterId { get; set; }
        internal Guid OwnerId { get; set; }
    }
}
