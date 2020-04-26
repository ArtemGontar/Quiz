using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Quizzes.Commands.Update
{
    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Guid>
    {
        private readonly ILogger<UpdateQuizCommandHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        public UpdateQuizCommandHandler(IRepository<Quiz> quizRepository,
            ILogger<UpdateQuizCommandHandler> logger)
        {
            _quizRepository = quizRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = new Quiz
            {
                Id = request.Id,
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
