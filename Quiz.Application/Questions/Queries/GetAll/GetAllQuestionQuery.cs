using System.Collections.Generic;
using AppQuiz.Domain;
using MediatR;

namespace AppQuiz.Application.Questions.Queries.GetAll
{
    public class GetAllQuestionQuery : IRequest<IEnumerable<Question>>
    {
    }
}
