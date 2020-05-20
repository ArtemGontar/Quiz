using AppQuiz.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppQuiz.Application.Chapters.Queries.GetByOwnerId
{
    public class GetChaptersByOwnerIdQuery : IRequest<IEnumerable<Chapter>>
    {
        public Guid OwnerId { get; set; }
    }
}
