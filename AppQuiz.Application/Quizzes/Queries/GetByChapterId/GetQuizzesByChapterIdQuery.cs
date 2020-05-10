using System;
using System.Collections.Generic;
using System.Text;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Quizzes.Queries.GetByChapterId
{
    public class GetQuizzesByChapterIdQuery : IRequest<IEnumerable<Quiz>>
    {
        public Guid ChapterId { get; set; }
    }
}
