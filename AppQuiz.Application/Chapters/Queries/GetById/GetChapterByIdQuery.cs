using System;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Chapters.Queries.GetById
{
    public class GetChapterByIdQuery : IRequest<Chapter>
    {
        public Guid ChapterId { get; set; }
    }
}
