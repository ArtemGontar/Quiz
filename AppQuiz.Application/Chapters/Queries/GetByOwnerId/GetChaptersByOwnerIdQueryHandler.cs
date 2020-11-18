using AppQuiz.Application.Chapters.Specifications;
using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Chapters.Queries.GetByOwnerId
{
    public class GetChaptersByOwnerIdQueryHandler : IRequestHandler<GetChaptersByOwnerIdQuery, IEnumerable<Chapter>>
    {
        private readonly ILogger<GetChaptersByOwnerIdQuery> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        public GetChaptersByOwnerIdQueryHandler(ILogger<GetChaptersByOwnerIdQuery> logger,
            IRepository<Chapter> chapterRepository)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
        }

        public async Task<IEnumerable<Chapter>> Handle(GetChaptersByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            var chapterSpecification = new ChapterByOwnerIdSpecification(request.OwnerId);
            return await _chapterRepository.GetAllAsync(chapterSpecification);
        }
    }
}
