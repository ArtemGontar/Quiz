using System.Collections.Generic;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Quizzes.Queries.GetAll
{
    public class GetAllQuizQuery : IRequest<IEnumerable<Quiz>>
    {
    }
}
