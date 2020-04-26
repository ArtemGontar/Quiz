using System;
using MediatR;

namespace AppQuiz.Application.Questions.Commands.Delete
{
    public class DeleteQuestionCommand : IRequest<bool>
    {
        internal Guid QuestionId { get; set; }

        public DeleteQuestionCommand(Guid questionId)
        {
            if (questionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(questionId));
            }

            QuestionId = questionId;
        }
    }
}
