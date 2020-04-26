using MediatR;
using System;

namespace AppQuiz.Application.Chapters.Commands.Delete
{
    public class DeleteChapterCommand : IRequest<bool>
    {
        internal Guid ChapterId { get; set; }

        public DeleteChapterCommand(Guid chapterId)
        {
            if (chapterId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(chapterId));
            }

            ChapterId = chapterId;
        }
    }
}
