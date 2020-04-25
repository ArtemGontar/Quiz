using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppQuiz.Application.Quizzes.Queries.GetAll
{
    public class GetAllQuizQueryHandler : IRequestHandler<GetAllQuizQuery, IEnumerable<Quiz>>
    {
        private readonly ILogger<GetAllQuizQueryHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        public GetAllQuizQueryHandler(IRepository<Quiz> quizRepository,
            ILogger<GetAllQuizQueryHandler> logger)
        {
            _quizRepository = quizRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<Quiz>> Handle(GetAllQuizQuery request, CancellationToken cancellationToken)
        {
            return await _quizRepository.GetAllAsync();
        }
    }
}
