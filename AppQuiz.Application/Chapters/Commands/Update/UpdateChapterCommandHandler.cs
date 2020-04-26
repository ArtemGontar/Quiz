using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Domain;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Application.Chapters.Commands.Update
{
    public class UpdateChapterCommandHandler : IRequestHandler<UpdateChapterCommand, Guid>
    {
        private readonly ILogger<UpdateChapterCommandHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        public UpdateChapterCommandHandler(ILogger<UpdateChapterCommandHandler> logger, 
            IRepository<Chapter> chapterRepository)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
        }


        public async Task<Guid> Handle(UpdateChapterCommand request, CancellationToken cancellationToken)
        {
            var chapter = new Chapter
            {
                Id = request.Id,
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
