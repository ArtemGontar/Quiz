using System;
using System.Linq;
using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Questions.Queries.GetByQuizId;
using AppQuiz.Application.Services;
using AutoMapper;
using MassTransit;
using Shared.Bus.Messages;
using IMediator = MediatR.IMediator;
using AppQuiz.Application.Quizzes.Specifications;

namespace AppQuiz.Application.Quizzes.Commands.Result
{
    public class ResultQuizCommandHandler : IRequestHandler<ResultQuizCommand, bool>
    {
        private readonly ILogger<ResultQuizCommandHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IMediator _mediator;
        private readonly ICheckResultService _checkResultService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;
        public ResultQuizCommandHandler(ILogger<ResultQuizCommandHandler> logger,
            IMediator mediator, 
            ISendEndpointProvider sendEndpointProvider, 
            ICheckResultService checkResultService,
            IRepository<Quiz> quizRepository,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _sendEndpointProvider = sendEndpointProvider;
            _checkResultService = checkResultService;
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ResultQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetAsync(new QuizByIdSpecification(request.QuizId));
            if (quiz == null)
            {
                _logger.LogError($"Quiz with id {request.QuizId} was not found");
                throw new InvalidOperationException($"Quiz with id {request.QuizId} was not found");
            }

            var questions = await _mediator.Send(new GetQuestionsByQuizIdQuery()
            {
                QuizId = request.QuizId
            }, cancellationToken);

            var quizResult = _checkResultService.CheckResult(questions.Select(x => x.CorrectAnswer).ToList(), request.Answers.ToList());

            //var message = _mapper.Map<QuizResultMessage>(quizResults);
            
            var message = new QuizResultMessage()
            {
                CorrectAnswersCount = quizResult.CorrectAnswersCount,
                CorrectPercent = quizResult.CorrectPercent,
                FailedAnswersCount = quizResult.WrongAnswersCount,
                QuestionsCount = quizResult.QuestionsCount,
                QuizId = quiz.Id,
                QuizTitle = quiz.Title,
                UserId = request.UserId,
                UserName = "Artem Hontar"
            };

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(message.GetReceiveEndpoint());
            await endpoint.Send(message, cancellationToken);

            return true;
        }
    }
}
