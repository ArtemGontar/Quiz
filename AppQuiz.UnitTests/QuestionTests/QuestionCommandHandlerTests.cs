using AppQuiz.Application.Questions.Commands.Create;
using AppQuiz.Application.Questions.Commands.Delete;
using AppQuiz.Application.Questions.Commands.Update;
using AppQuiz.Application.Questions.Queries.GetAll;
using AppQuiz.Application.Questions.Queries.GetById;
using AppQuiz.Domain;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Persistence.MongoDb;
using Xunit;

namespace AppQuiz.UnitTests.QuestionTests
{
    public class QuestionCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private Mock<IRepository<Question>> _questionRepository;
        public QuestionCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact]
        public async Task CreateCommandHandler_HandleSuccess()
        {
            //Arrange
            var createQuestionCommandHandler = _autoMocker.CreateInstance<CreateQuestionCommandHandler>();
            _questionRepository = _autoMocker.GetMock<IRepository<Question>>();
            _questionRepository.Setup(x => x.SaveAsync(It.IsAny<Question>()))
                .ReturnsAsync(true);
            var createQuestionCommand = new CreateQuestionCommand()
            {
                QuizId = Guid.NewGuid(),
                Title = "anonymousText",
                CorrectAnswer = "anonymousText",
                Options = new List<Option>()
                {
                    new Option(){Value = "anonymousOption1"},
                    new Option(){Value = "anonymousOption2"},
                    new Option(){Value = "anonymousOption3"},
                    new Option(){Value = "anonymousOption4"},
                }
            };

            //Act
            var result = await createQuestionCommandHandler.Handle(createQuestionCommand, CancellationToken.None);

            //Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task UpdateCommandHandler_HandleSuccess()
        {
            //Arrange
            var updateQuestionCommandHandler = _autoMocker.CreateInstance<UpdateQuestionCommandHandler>();
            _questionRepository = _autoMocker.GetMock<IRepository<Question>>();
            _questionRepository.Setup(x => x.SaveAsync(It.IsAny<Question>()))
                .ReturnsAsync(true);
            var updateQuestionCommand = new UpdateQuestionCommand()
            {
                QuizId = Guid.NewGuid(),
                Text = "anonymousText",
                CorrectAnswer = "anonymousText",
                Options = new List<string>()
                {
                    "anonymousOption1",
                    "anonymousOption2",
                    "anonymousOption3",
                }
            };
            updateQuestionCommand.SetId(Guid.NewGuid());

            //Act
            var result = await updateQuestionCommandHandler.Handle(updateQuestionCommand, CancellationToken.None);

            //Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task DeleteCommandHandler_HandleSuccess()
        {
            //Arrange
            var deleteQuestionCommandHandler = _autoMocker.CreateInstance<DeleteQuestionCommandHandler>();
            _questionRepository = _autoMocker.GetMock<IRepository<Question>>();
            _questionRepository.Setup(x => x.DeleteAsync(It.IsAny<ISpecification<Question>>()))
                .ReturnsAsync(true);
            var deleteQuestionCommand = new DeleteQuestionCommand(Guid.NewGuid());

            //Act
            var result = await deleteQuestionCommandHandler.Handle(deleteQuestionCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllQuizQueryHandler_HandleSuccess()
        {
            //Arrange
            var getAllQuestionQueryHandler = _autoMocker.CreateInstance<GetAllQuestionQueryHandler>();
            _questionRepository = _autoMocker.GetMock<IRepository<Question>>();
            _questionRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Question>());
            var getAllQuestionQuery = new GetAllQuestionQuery();


            //Act
            var result = await getAllQuestionQueryHandler.Handle(getAllQuestionQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetQuizByIdQueryHandler_HandleSuccess()
        {
            //Arrange
            var getQuestionByIdQueryHandler = _autoMocker.CreateInstance<GetQuestionByIdQueryHandler>();
            _questionRepository = _autoMocker.GetMock<IRepository<Question>>();
            _questionRepository.Setup(x => x.GetAsync(It.IsAny<ISpecification<Question>>()))
                .ReturnsAsync(new Question());
            var getAllQuestionQuery = new GetQuestionByIdQuery()
            {
                QuestionId = Guid.NewGuid()
            };

            //Act
            var result = await getQuestionByIdQueryHandler.Handle(getAllQuestionQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
        }
    }
}
