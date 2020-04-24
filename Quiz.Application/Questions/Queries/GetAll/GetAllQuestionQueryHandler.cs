using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Domain;
using AppQuiz.Persistence.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppQuiz.Application.Questions.Queries.GetAll
{
    public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, IEnumerable<Question>>
    {
        private readonly ILogger<GetAllQuestionQueryHandler> _logger;
        private readonly IRepository<Question> _questionRepository;
        public GetAllQuestionQueryHandler(IRepository<Question> questionRepository,
            ILogger<GetAllQuestionQueryHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<Question>> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
        {
            return await _questionRepository.GetAllAsync();
        }
    }
}
