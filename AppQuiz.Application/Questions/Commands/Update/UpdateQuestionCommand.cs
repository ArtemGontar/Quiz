using System;
using System.Collections.Generic;
using MediatR;

namespace AppQuiz.Application.Questions.Commands.Update
{
    public class UpdateQuestionCommand : IRequest<Guid>
    {
        internal Guid Id { get; set; }
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public Guid QuizId { get; set; }

        public void SetId(Guid questionId)
        {
            if (questionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(questionId));
            }

            Id = questionId;
        }
    }
}
