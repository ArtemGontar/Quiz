using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Application.Chapters.Commands.Create
{
    public class CreateChapterCommandHandler : IRequestHandler<CreateChapterCommand, Guid>
    {
        private readonly ILogger<CreateChapterCommandHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        public CreateChapterCommandHandler(ILogger<CreateChapterCommandHandler> logger, 
            IRepository<Chapter> chapterRepository)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
        }

        public async Task<Guid> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            var chapter = new Chapter()
            {
                Name = request.Name
            };

            if (!await _chapterRepository.SaveAsync(chapter))
            {
                _logger.LogError("Chapter save failed");
                throw new InvalidOperationException("Chapter save failed");
            }

            return chapter.Id;
        }
    }
}
