using System;
using System.Collections.Generic;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Questions.Queries.GetByQuizId
{
    public class GetQuestionsByQuizIdQuery : IRequest<IEnumerable<Question>>
    {
        public Guid QuizId { get; set; }
    }
}
