using System;
using System.Collections.Generic;
using MediatR;

namespace AppQuiz.Application.Questions.Commands.Create
{
    public class CreateQuestionCommand : IRequest<Guid>
    {
        public string Text { get; set; }
        //Add validation for CorrectAnswer contains in Options
        public string CorrectAnswer { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public Guid QuizId { get; set; }
    }
}
