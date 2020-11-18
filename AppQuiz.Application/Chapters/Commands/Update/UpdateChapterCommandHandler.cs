using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Domain;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using AppQuiz.Application.Chapters.Specifications;

namespace AppQuiz.Application.Chapters.Commands.Update
{
    public class UpdateChapterCommandHandler : IRequestHandler<UpdateChapterCommand, Guid>
    {
        private readonly ILogger<UpdateChapterCommandHandler> _logger;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IMapper _mapper;
        public UpdateChapterCommandHandler(ILogger<UpdateChapterCommandHandler> logger, 
            IRepository<Chapter> chapterRepository, IMapper mapper)
        {
            _logger = logger;
            _chapterRepository = chapterRepository;
            _mapper = mapper;
        }


        public async Task<Guid> Handle(UpdateChapterCommand request, CancellationToken cancellationToken)
        {
            if(!await _chapterRepository.AnyAsync(new ChapterByIdSpecification(request.Id)))
            {
                _logger.LogError($"Chapter with id {request.Id} not found");
                throw new InvalidOperationException($"Chapter with id {request.Id} not found");
            }

            var chapter = _mapper.Map<Chapter>(request);

            if (!await _chapterRepository.SaveAsync(chapter))
            {
                _logger.LogError("Update chapter failed");
                throw new InvalidOperationException("Update chapter failed");
            }

            return chapter.Id;
        }
    }
}
