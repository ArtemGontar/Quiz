using System;
using MediatR;

namespace AppQuiz.Application.Quizzes.Commands.Create
{
    public class CreateQuizCommand : IRequest<Guid>
    {
        public CreateQuizCommand(Guid ownerId)
        {
            if (ownerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(ownerId));
            }

            OwnerId = ownerId;
        }
        public string Title { get; set; }
        internal Guid OwnerId { get; set; }
    }
}
