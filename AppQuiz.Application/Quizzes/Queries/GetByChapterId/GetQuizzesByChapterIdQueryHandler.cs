using AppQuiz.Application.Chapters.Specifications;
using AppQuiz.Application.Quizzes.Queries.GetAll;
using AppQuiz.Application.Quizzes.Specifications;
using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppQuiz.Application.Quizzes.Queries.GetByChapterId
{
    public class GetQuizByChapterIdQueryHandler : IRequestHandler<GetQuizzesByChapterIdQuery, IEnumerable<Quiz>>
    {
        private readonly ILogger<GetAllQuizQueryHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<Chapter> _chapterRepository;

        public GetQuizByChapterIdQueryHandler(ILogger<GetAllQuizQueryHandler> logger, 
            IRepository<Quiz> quizRepository, 
            IRepository<Chapter> chapterRepository)
        {
            _logger = logger;
            _quizRepository = quizRepository;
            _chapterRepository = chapterRepository;
        }
        public async Task<IEnumerable<Quiz>> Handle(GetQuizzesByChapterIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _chapterRepository.AnyAsync(new ChapterByIdSpecification(request.ChapterId)))
            {
                throw new InvalidOperationException();
            }
            var chapterIdSpecification = new QuizByChapterIdSpecification(request.ChapterId);
            return await _quizRepository.GetAllAsync(chapterIdSpecification);
        }
    }
}
