using System;
using System.Collections.Generic;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Questions.Commands.Create
{
    public class CreateQuestionCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        //Add validation for CorrectAnswer contains in Options
        public string CorrectAnswer { get; set; }
        public IEnumerable<Option> Options { get; set; } = new List<Option>();
        public Guid QuizId { get; set; }
    }
}
