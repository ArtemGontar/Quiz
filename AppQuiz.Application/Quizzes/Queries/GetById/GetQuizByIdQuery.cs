using System;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Quizzes.Queries.GetById
{
    public class GetQuizByIdQuery : IRequest<Quiz>
    {
        public Guid QuizId { get; set; }
    }
}
