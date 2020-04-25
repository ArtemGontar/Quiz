using System;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Questions.Queries.GetById
{
    public class GetQuestionByIdQuery : IRequest<Question>
    {
        public Guid QuestionId { get; set; }
    }
}
