using AppQuiz.Domain;
using MediatR;
using System;
using System.Collections.Generic;

namespace AppQuiz.Application.Quizzes.Commands.Result
{
    public class ResultQuizCommand : IRequest<bool>
    {
        public ResultQuizCommand(Guid quizId, Guid userId)
        {
            if (quizId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(quizId));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            QuizId = quizId;
            UserId = userId;
        }
        internal Guid QuizId { get; set; }
        internal Guid UserId { get; set; }
        public IEnumerable<string> Answers { get; set; } = new List<string>();

    }
}
