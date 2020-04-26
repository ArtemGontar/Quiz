using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Application.Chapters.Queries.GetAll
{
    public class GetAllChapterQueryHandler : IRequestHandler<GetAllChapterQuery, IEnumerable<Chapter>>
    {
        private readonly ILogger<GetAllChapterQueryHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        public GetAllChapterQueryHandler(ILogger<GetAllChapterQueryHandler> logger, 
            IRepository<Chapter> chapterRepository)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
        }


        public async Task<IEnumerable<Chapter>> Handle(GetAllChapterQuery request, CancellationToken cancellationToken)
        {
            return await _chapterRepository.GetAllAsync();
        }
    }
}
