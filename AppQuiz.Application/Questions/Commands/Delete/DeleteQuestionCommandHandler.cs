using AppQuiz.Application.Questions.Specifications;
using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Questions.Commands.Delete
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, bool>
    {
        private readonly ILogger<DeleteQuestionCommandHandler> _logger;
        private readonly IRepository<Question> _questionRepository;
        public DeleteQuestionCommandHandler(IRepository<Question> questionRepository,
            ILogger<DeleteQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var questionSpecification = new QuestionByIdSpecification(request.QuestionId);
            if (!await _questionRepository.AnyAsync(questionSpecification))
            {
                _logger.LogError($"Question with id {request.QuestionId} not found");
                throw new InvalidOperationException($"Question with id {request.QuestionId} not found");
            }

            if (!await _questionRepository.DeleteAsync(questionSpecification))
            {
                _logger.LogError($"Delete question failed");
                throw new InvalidOperationException($"Delete question failed");
            }

            return true;
        }
    }
}
