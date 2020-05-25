using AppQuiz.Domain;
using MediatR;
using System;
using System.Collections.Generic;

namespace AppQuiz.Application.Quizzes.Commands.Result
{
    public class ResultQuizCommand : IRequest<bool>
    {
        public ResultQuizCommand(Guid quizId)
        {
            if (quizId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(quizId));
            }
            QuizId = quizId;
        }
        internal Guid QuizId { get; set; }
        public IEnumerable<string> Answers { get; set; } = new List<string>();

    }
}
