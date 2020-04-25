using System;
using MediatR;

namespace AppQuiz.Application.Questions.Commands.Delete
{
    public class DeleteQuestionCommand : IRequest<bool>
    {
        internal Guid QuestionId { get; set; }

        public DeleteQuestionCommand(Guid quizId)
        {
            if (quizId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(quizId));
            }

            QuestionId = quizId;
        }
    }
}
