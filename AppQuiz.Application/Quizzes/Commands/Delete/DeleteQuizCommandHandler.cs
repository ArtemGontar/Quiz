using AppQuiz.Application.Quizzes.Specifications;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Quizzes.Commands.Delete
{
    public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, bool>
    {
        private readonly ILogger<DeleteQuizCommandHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        public DeleteQuizCommandHandler(IRepository<Quiz> quizRepository,
            ILogger<DeleteQuizCommandHandler> logger)
        {
            _quizRepository = quizRepository;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            var quizSpecification = new QuizByIdSpecification(request.QuizId);
            return await _quizRepository.DeleteAsync(quizSpecification);
        }
    }
}
