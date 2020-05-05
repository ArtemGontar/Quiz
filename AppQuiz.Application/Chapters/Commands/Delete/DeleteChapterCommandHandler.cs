using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Chapters.Specifications;
using AppQuiz.Domain;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using MassTransit;
using Shared.Bus.Messages;
using AutoMapper;

namespace AppQuiz.Application.Chapters.Commands.Delete
{
    public class DeleteChapterCommandHandler : IRequestHandler<DeleteChapterCommand, bool>
    {
        private readonly ILogger<DeleteChapterCommandHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;
        public DeleteChapterCommandHandler(ILogger<DeleteChapterCommandHandler> logger, 
            IRepository<Chapter> chapterRepository, 
            ISendEndpointProvider sendEndpointProvider,
            IMapper mapper)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
            _sendEndpointProvider = sendEndpointProvider;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            var chapterSpecification = new ChapterByIdSpecification(request.ChapterId);
            if (!await _chapterRepository.AnyAsync(chapterSpecification))
            {
                _logger.LogError($"Chapter with id {request.ChapterId} not found");
                throw new InvalidOperationException($"Chapter with id {request.ChapterId} not found");
            }

            if(!await _chapterRepository.DeleteAsync(chapterSpecification))
            {
                _logger.LogError($"Delete chapter failed");
                throw new InvalidOperationException($"Delete chapter failed");
            }

            var message = _mapper.Map<DeleteChapterMessage>(request);
            await _sendEndpointProvider.Send(message, cancellationToken);

            return true;
        }
    }
}
