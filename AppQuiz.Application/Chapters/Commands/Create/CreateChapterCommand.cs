using MediatR;
using System;
using Shared.Common;

namespace AppQuiz.Application.Chapters.Commands.Create
{
    public class CreateChapterCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public EnglishLevel EnglishLevel { get; set; }
    }
}
