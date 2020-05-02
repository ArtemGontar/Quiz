using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Quizzes.Specifications;
using AutoMapper;

namespace AppQuiz.Application.Questions.Commands.Create
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Guid>
    {
        private readonly ILogger<CreateQuestionCommandHandler> _logger;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IMapper _mapper;
        public CreateQuestionCommandHandler(IRepository<Question> questionRepository,
            ILogger<CreateQuestionCommandHandler> logger,
            IRepository<Quiz> quizRepository,
            IMapper mapper)
        {
            _questionRepository = questionRepository;
            _logger = logger;
            _quizRepository = quizRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            if (!await _quizRepository.AnyAsync(new QuizByIdSpecification(request.QuizId)))
            {
                throw new InvalidOperationException();
            }
            var question = _mapper.Map<Question>(request);

            if (!await _questionRepository.SaveAsync(question))
            {
                _logger.LogError("Question save failed");
                throw new InvalidOperationException("Question save failed");
            }

            return question.Id;
        }
    }
}
