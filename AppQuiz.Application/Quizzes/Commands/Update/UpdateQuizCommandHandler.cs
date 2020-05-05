using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Chapters.Specifications;
using AutoMapper;
using AppQuiz.Application.Quizzes.Specifications;
using AppQuiz.Application.Questions.Commands.Update;

namespace AppQuiz.Application.Quizzes.Commands.Update
{
    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Guid>
    {
        private readonly ILogger<UpdateQuizCommandHandler> _logger;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateQuizCommandHandler(IRepository<Quiz> quizRepository,
            ILogger<UpdateQuizCommandHandler> logger, 
            IRepository<Chapter> chapterRepository, 
            IMapper mapper, IMediator mediator)
        {
            _quizRepository = quizRepository;
            _logger = logger;
            _chapterRepository = chapterRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            if (!await _chapterRepository.AnyAsync(new ChapterByIdSpecification(request.ChapterId)))
            {
                _logger.LogError($"Chapter with id {request.ChapterId} was not found");
                throw new InvalidOperationException($"Chapter with id {request.ChapterId} was not found");
            }

            if (!await _quizRepository.AnyAsync(new QuizByIdSpecification(request.Id)))
            {
                _logger.LogError($"Quiz with id {request.ChapterId} was not found");
                throw new InvalidOperationException($"Quiz with id {request.ChapterId} was not found");
            }

            var quiz = _mapper.Map<Quiz>(request);

            if (!await _quizRepository.SaveAsync(quiz))
            {
                _logger.LogError("Update quiz failed");
                throw new InvalidOperationException("Update quiz failed");
            }

            foreach (var question in request.Questions)
            {
                var updateQuestionCommand = _mapper.Map<UpdateQuestionCommand>(question);
                updateQuestionCommand.QuizId = quiz.Id;

                await _mediator.Send(updateQuestionCommand, cancellationToken);
            }

            return quiz.Id;
        }
    }
}
