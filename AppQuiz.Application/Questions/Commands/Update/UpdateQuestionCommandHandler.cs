using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Questions.Commands.Update
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Guid>
    {
        private readonly ILogger<UpdateQuestionCommandHandler> _logger;
        private readonly IRepository<Question> _questionRepository;
        public UpdateQuestionCommandHandler(IRepository<Question> questionRepository,
            ILogger<UpdateQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }
        public async Task<Guid> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question
            {
                Id = request.Id,
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
