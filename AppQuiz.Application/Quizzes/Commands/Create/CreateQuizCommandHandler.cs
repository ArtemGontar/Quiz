using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Chapters.Specifications;
using AppQuiz.Application.Questions.Commands.Create;
using AutoMapper;

namespace AppQuiz.Application.Quizzes.Commands.Create
{
    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Guid>
    {
        private readonly ILogger<CreateQuizCommandHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CreateQuizCommandHandler(IRepository<Quiz> quizRepository,
            ILogger<CreateQuizCommandHandler> logger, 
            IRepository<Chapter> chapterRepository,
            IMapper mapper, IMediator mediator)
        {
            _quizRepository = quizRepository;
            _logger = logger;
            _chapterRepository = chapterRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            if (!await _chapterRepository.AnyAsync(new ChapterByIdSpecification(request.ChapterId)))
            {
                _logger.LogError($"Chapter with id {request.ChapterId} was not found");
                throw new InvalidOperationException($"Chapter with id {request.ChapterId} was not found");
            }

            var quiz = _mapper.Map<Quiz>(request);
            
            if (!await _quizRepository.SaveAsync(quiz))
            {
                _logger.LogError("Create quiz failed");
                throw new InvalidOperationException("Create quiz failed");
            }

            foreach (var question in request.Questions)
            {
                var createQuestionCommand = _mapper.Map<CreateQuestionCommand>(question);
                createQuestionCommand.QuizId = quiz.Id;

                await _mediator.Send(createQuestionCommand, cancellationToken);
            }

            return quiz.Id;
        }
    }
}
