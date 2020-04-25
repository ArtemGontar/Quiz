using System;
using MediatR;

namespace AppQuiz.Application.Quizzes.Commands.Delete
{
    public class DeleteQuizCommand : IRequest<bool>
    {
        internal Guid QuizId { get; set; }

        public DeleteQuizCommand(Guid quizId)
        {
            if (quizId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(quizId));
            }

            QuizId = quizId;
        }
    }
}
