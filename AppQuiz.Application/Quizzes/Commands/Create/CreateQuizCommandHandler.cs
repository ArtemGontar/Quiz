using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Quizzes.Commands.Create
{
    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Guid>
    {
        private readonly ILogger<CreateQuizCommandHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        public CreateQuizCommandHandler(IRepository<Quiz> quizRepository,
            ILogger<CreateQuizCommandHandler> logger)
        {
            _quizRepository = quizRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = new Quiz
            {
                Title = request.Title,
                OwnerId = request.OwnerId
            };

            if (!await _quizRepository.SaveAsync(quiz))
            {
                _logger.LogError("Quiz save failed");
                throw new InvalidOperationException("Quiz save failed");
            }

            return quiz.Id;
        }
    }
}
