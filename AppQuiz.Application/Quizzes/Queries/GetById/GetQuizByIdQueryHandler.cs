using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Quizzes.Specifications;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppQuiz.Application.Quizzes.Queries.GetById
{
    public class GetQuizByIdQueryHandler : IRequestHandler<GetQuizByIdQuery, Quiz>
    {
        private readonly ILogger<GetQuizByIdQueryHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        public GetQuizByIdQueryHandler(IRepository<Quiz> quizRepository,
            ILogger<GetQuizByIdQueryHandler> logger)
        {
            _quizRepository = quizRepository;
            _logger = logger;
        }
        public async Task<Quiz> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
        {
            var quizSpecification = new QuizByIdSpecification(request.QuizId);
            var quiz = await _quizRepository.GetAsync(quizSpecification);
            return quiz;
        }
    }
}
