using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Questions.Specifications;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

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
            var question = await _questionRepository.GetAsync(questionSpecification);
            return question;
        }
    }
}
