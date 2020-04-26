using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Questions.Commands.Create
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Guid>
    {
        private readonly ILogger<CreateQuestionCommandHandler> _logger;
        private readonly IRepository<Question> _questionRepository;
        public CreateQuestionCommandHandler(IRepository<Question> questionRepository,
            ILogger<CreateQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question
            {
                QuizId = request.QuizId,
                CorrectAnswer = request.CorrectAnswer,
                Options = request.Options,
                Text = request.Text
            };

            if (!await _questionRepository.SaveAsync(question))
            {
                _logger.LogError("Question save failed");
                throw new InvalidOperationException("Question save failed");
            }

            return question.Id;
        }
    }
}
