using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Chapters.Specifications;
using AutoMapper;

namespace AppQuiz.Application.Quizzes.Commands.Update
{
    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Guid>
    {
        private readonly ILogger<UpdateQuizCommandHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IMapper _mapper;

        public UpdateQuizCommandHandler(IRepository<Quiz> quizRepository,
            ILogger<UpdateQuizCommandHandler> logger, 
            IRepository<Chapter> chapterRepository, 
            IMapper mapper)
        {
            _quizRepository = quizRepository;
            _logger = logger;
            _chapterRepository = chapterRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            if (!await _chapterRepository.AnyAsync(new ChapterByIdSpecification(request.ChapterId)))
            {
                throw new InvalidOperationException();
            }

            var quiz = _mapper.Map<Quiz>(request);

            if (!await _quizRepository.SaveAsync(quiz))
            {
                _logger.LogError("Quiz save failed");
                throw new InvalidOperationException("Quiz save failed");
            }

            return quiz.Id;
        }
    }
}
