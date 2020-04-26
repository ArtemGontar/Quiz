using AppQuiz.Domain;
using MediatR;
using System.Collections.Generic;

namespace AppQuiz.Application.Chapters.Queries.GetAll
{
    public class GetAllChapterQuery : IRequest<IEnumerable<Chapter>>
    {
    }
}
