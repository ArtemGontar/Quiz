using System;
using System.Collections.Generic;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Questions.Commands.Update
{
    public class UpdateQuestionCommand : IRequest<Guid>
    {
        internal Guid Id { get; set; }
        public string Title { get; set; }
        public string CorrectAnswer { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        public Guid QuizId { get; set; }

        public void SetQuestionId(Guid questionId)
        {
            if (questionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(questionId));
            }

            Id = questionId;
        }
    }
}
