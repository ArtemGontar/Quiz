using MediatR;
using System;
using Shared.Common;

namespace AppQuiz.Application.Chapters.Commands.Update
{
    public class UpdateChapterCommand : IRequest<Guid>
    {
        internal Guid Id { get; set; }
        
        public string Name { get; set; }

        public EnglishLevel EnglishLevel { get; set; }
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
