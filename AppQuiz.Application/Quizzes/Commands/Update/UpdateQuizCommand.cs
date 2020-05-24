using System;
using System.Collections.Generic;
using AppQuiz.Domain;
using MediatR;
using Shared.Common;

namespace AppQuiz.Application.Quizzes.Commands.Update
{
    public class UpdateQuizCommand : IRequest<Guid>
    {
        internal Guid Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        public IEnumerable<Question> Questions { get; set; } = new List<Question>();
        public Guid ChapterId { get; set; }
        internal Guid OwnerId { get; set; }

        public void SetIdAndOwnerId(Guid quizId, Guid ownerId)
        {
            if (quizId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(quizId));
            }

            if (ownerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(ownerId));
            }

            OwnerId = ownerId;
            Id = quizId;
        }
    }
}
