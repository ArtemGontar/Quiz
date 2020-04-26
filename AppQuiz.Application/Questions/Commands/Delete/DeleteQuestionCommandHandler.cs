using AppQuiz.Application.Questions.Specifications;
using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
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
            return await _questionRepository.DeleteAsync(questionSpecification);
        }
    }
}
