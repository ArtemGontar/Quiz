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

namespace AppQuiz.Application.Quizzes.Commands.Result
{
    public class ResultQuizCommandHandler : IRequestHandler<ResultQuizCommand, bool>
    {
        private readonly ILogger<ResultQuizCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ICheckResultService _checkResultService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;
        public ResultQuizCommandHandler(ILogger<ResultQuizCommandHandler> logger,
            IMediator mediator, 
            ISendEndpointProvider sendEndpointProvider, 
            ICheckResultService checkResultService, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _sendEndpointProvider = sendEndpointProvider;
            _checkResultService = checkResultService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ResultQuizCommand request, CancellationToken cancellationToken)
        {
            var questions = await _mediator.Send(new GetQuestionsByQuizIdQuery()
            {
                QuizId = request.QuizId
            }, cancellationToken);

            var quizResults = _checkResultService.CheckResult(questions.Select(x => x.CorrectAnswer).ToList(), request.Answers.ToList());

            var message = _mapper.Map<QuizResultMessage>(quizResults);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(message.GetReceiveEndpoint());
            await endpoint.Send(message, cancellationToken);

            return true;
        }
    }
}
