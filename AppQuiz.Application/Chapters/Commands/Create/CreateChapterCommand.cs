using MediatR;
using System;

namespace AppQuiz.Application.Chapters.Commands.Create
{
    public class CreateChapterCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
