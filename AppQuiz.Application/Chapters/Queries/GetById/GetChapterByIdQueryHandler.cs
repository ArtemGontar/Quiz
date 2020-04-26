using AppQuiz.Application.Chapters.Specifications;
using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Chapters.Queries.GetById
{
    public class GetChapterByIdQueryHandler : IRequestHandler<GetChapterByIdQuery, Chapter>
    {
        private readonly ILogger<GetChapterByIdQueryHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        public GetChapterByIdQueryHandler(ILogger<GetChapterByIdQueryHandler> logger, 
            IRepository<Chapter> chapterRepository)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
        }


        public async Task<Chapter> Handle(GetChapterByIdQuery request, CancellationToken cancellationToken)
        {
            var chapterSpecification = new ChapterByIdSpecification(request.ChapterId);
            return await _chapterRepository.GetAsync(chapterSpecification);
        }
    }
}
