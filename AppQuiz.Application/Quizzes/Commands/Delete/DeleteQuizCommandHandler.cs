using AppQuiz.Application.Quizzes.Specifications;
using AppQuiz.Domain;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Bus.Messages;
using Shared.Bus.Messages.Messages;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Quizzes.Commands.Delete
{
    public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, bool>
    {
        private readonly ILogger<DeleteQuizCommandHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;
        public DeleteQuizCommandHandler(IRepository<Quiz> quizRepository,
            ILogger<DeleteQuizCommandHandler> logger,
            ISendEndpointProvider sendEndpointProvider,
            IMapper mapper)
        {
            _quizRepository = quizRepository;
            _logger = logger;
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task<bool> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            var quizSpecification = new QuizByIdSpecification(request.QuizId);
            if (!await _quizRepository.AnyAsync(quizSpecification))
            {
                _logger.LogError($"Quiz with id {request.QuizId} not found");
                throw new InvalidOperationException($"Chapter with id {request.QuizId} not found");
            }
            if (!await _quizRepository.DeleteAsync(quizSpecification))
            {
                _logger.LogError($"Delete chapter failed");
                throw new InvalidOperationException($"Delete chapter failed");
            }

            var message = _mapper.Map<DeleteQuizMessage>(request);
            await _sendEndpointProvider.Send(message, cancellationToken);

            return true;
        }
    }
}
