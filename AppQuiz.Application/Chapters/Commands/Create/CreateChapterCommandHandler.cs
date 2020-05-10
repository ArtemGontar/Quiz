using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Application.Chapters.Commands.Create
{
    public class CreateChapterCommandHandler : IRequestHandler<CreateChapterCommand, Guid>
    {
        private readonly ILogger<CreateChapterCommandHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IMapper _mapper;
        public CreateChapterCommandHandler(ILogger<CreateChapterCommandHandler> logger, 
            IRepository<Chapter> chapterRepository, IMapper mapper)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            var chapter = _mapper.Map<Chapter>(request);

            if (!await _chapterRepository.SaveAsync(chapter))
            {
                _logger.LogError("Create chapter failed");
                throw new InvalidOperationException("Create chapter failed");
            }

            return chapter.Id;
        }
    }
}
