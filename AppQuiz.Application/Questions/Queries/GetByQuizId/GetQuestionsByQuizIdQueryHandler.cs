using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppQuiz.Application.Questions.Queries.GetAll;
using AppQuiz.Application.Questions.Specifications;
using AppQuiz.Application.Quizzes.Specifications;
using AppQuiz.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Persistence.MongoDb;

namespace AppQuiz.Application.Questions.Queries.GetByQuizId
{
    public class GetQuestionsByQuizIdQueryHandler : IRequestHandler<GetQuestionsByQuizIdQuery, IEnumerable<Question>>
    {
        private readonly ILogger<GetAllQuestionQueryHandler> _logger;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Quiz> _quizRepository;

        public GetQuestionsByQuizIdQueryHandler(ILogger<GetAllQuestionQueryHandler> logger, 
            IRepository<Question> questionRepository, 
            IRepository<Quiz> quizRepository)
        {
            _logger = logger;
            _questionRepository = questionRepository;
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<Question>> Handle(GetQuestionsByQuizIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _quizRepository.AnyAsync(new QuizByIdSpecification(request.QuizId)))
            {
                throw new InvalidOperationException();
            }

            var questionByQuizIdSpecification = new QuestionsByQuizIdSpecification(request.QuizId);
            return await _questionRepository.GetAllAsync(questionByQuizIdSpecification);
        }
    }
}
