using AppQuiz.Application.Questions.Specifications;
using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Questions.Queries.GetById
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, Question>
    {
        private readonly ILogger<GetQuestionByIdQueryHandler> _logger;
        private readonly IRepository<Question> _questionRepository;
        public GetQuestionByIdQueryHandler(IRepository<Question> questionRepository,
            ILogger<GetQuestionByIdQueryHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }
        public async Task<Question> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var questionSpecification = new QuestionByIdSpecification(request.QuestionId);
            return await _questionRepository.GetAsync(questionSpecification);
        }
    }
}
