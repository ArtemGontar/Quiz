using System;
using MediatR;

namespace AppQuiz.Application.Quizzes.Commands.Update
{
    public class UpdateQuizCommand : IRequest<Guid>
    {
        internal Guid Id { get; set; }
        public string Title { get; set; }
        internal Guid OwnerId { get; set; }

        public void SetIdAndOwnerId(Guid quizId, Guid ownerId)
        {
            if (quizId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(quizId));
            }

            if (ownerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(ownerId));
            }

            OwnerId = ownerId;
            Id = quizId;
        }
    }
}
