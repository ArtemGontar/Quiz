using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Quizzes.Specifications;
using AutoMapper;
using AppQuiz.Application.Questions.Specifications;

namespace AppQuiz.Application.Questions.Commands.Update
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Guid>
    {
        private readonly ILogger<UpdateQuestionCommandHandler> _logger;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IMapper _mapper;
        public UpdateQuestionCommandHandler(IRepository<Question> questionRepository,
            ILogger<UpdateQuestionCommandHandler> logger, IRepository<Quiz> quizRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _logger = logger;
            _quizRepository = quizRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            if (!await _quizRepository.AnyAsync(new QuizByIdSpecification(request.QuizId)))
            {
                _logger.LogError($"Quiz with id {request.QuizId} was not found");
                throw new InvalidOperationException($"Quiz with id {request.QuizId} was not found");
            }

            if (!await _questionRepository.AnyAsync(new QuestionByIdSpecification(request.Id)))
            {
                _logger.LogError($"Question with id {request.Id} was not found");
                throw new InvalidOperationException($"Question with id {request.Id} was not found");
            }

            var question = _mapper.Map<Question>(request);

            if (!await _questionRepository.SaveAsync(question))
            {
                _logger.LogError("Update question failed");
                throw new InvalidOperationException("Update question failed");
            }

            return question.Id;
        }
    }
}
