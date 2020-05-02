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

namespace AppQuiz.Application.Chapters.Commands.Delete
{
    public class DeleteChapterCommandHandler : IRequestHandler<DeleteChapterCommand, bool>
    {
        private readonly ILogger<DeleteChapterCommandHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public DeleteChapterCommandHandler(ILogger<DeleteChapterCommandHandler> logger, 
            IRepository<Chapter> chapterRepository, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<bool> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            var chapterSpecification = new ChapterByIdSpecification(request.ChapterId);
            
            
            var message = new DeleteChapterMessage()
            {
                ChapterId = request.ChapterId
            };
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(message.GetReceiveEndpoint());
            await endpoint.Send(message, cancellationToken);
            return await _chapterRepository.DeleteAsync(chapterSpecification);
        }
    }
}
