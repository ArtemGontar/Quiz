using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Chapters.Specifications;
using AppQuiz.Domain;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Application.Chapters.Commands.Delete
{
    public class DeleteChapterCommandHandler : IRequestHandler<DeleteChapterCommand, bool>
    {
        private readonly ILogger<DeleteChapterCommandHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        public DeleteChapterCommandHandler(ILogger<DeleteChapterCommandHandler> logger, 
            IRepository<Chapter> chapterRepository)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
        }

        public async Task<bool> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            var chapterSpecification = new ChapterByIdSpecification(request.ChapterId);
            return await _chapterRepository.DeleteAsync(chapterSpecification);
        }
    }
}
