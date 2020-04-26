using MediatR;
using System;

namespace AppQuiz.Application.Chapters.Commands.Update
{
    public class UpdateChapterCommand : IRequest<Guid>
    {
        internal Guid Id { get; set; }
        public string Name { get; set; }
        public void SetId(Guid chapterId)
        {
            if (chapterId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(chapterId));
            }

            Id = chapterId;
        }
    }
}
