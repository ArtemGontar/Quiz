using AppQuiz.Application.Infrastructure;
using AppQuiz.Application.Questions.Commands.Update;
using AppQuiz.Domain;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using Shared.Persistence.MongoDb;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AppQuiz.UnitTests.QuestionTests
{
    public class UpdateQuestionCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private Mock<IRepository<Question>> _questionRepositoryMock;
        private Mock<IRepository<Quiz>> _quizRepositoryMock;
        private UpdateQuestionCommandHandler _questionCommandHandler;
        public UpdateQuestionCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _autoMocker.Use<IMapper>(new MapperConfiguration(x => x.AddMaps(typeof(QuizProfile).Assembly)).CreateMapper());
            _questionCommandHandler = _autoMocker.CreateInstance<UpdateQuestionCommandHandler>();
            _questionRepositoryMock = _autoMocker.GetMock<IRepository<Question>>();
            _quizRepositoryMock = _autoMocker.GetMock<IRepository<Quiz>>();
        }

        [Fact]
        public async Task Handler_ValidQuestionData_ShouldSuccess()
        {
            //Arrange
            _quizRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(true);
            _questionRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Question>>()))
                .ReturnsAsync(true);
            _questionRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<Question>()))
                .Callback<Question>(x => x.Id = Guid.NewGuid())
                .ReturnsAsync(true);

            var createQuestionCommand = new UpdateQuestionCommand()
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
            var result = await _questionCommandHandler.Handle(createQuestionCommand, CancellationToken.None);

            //Assert
            Assert.NotEqual(Guid.Empty, result);
        }


        [Fact]
        public async Task Handle_QuizIdNotExist_ShouldNotFound()
        {
            //Arrange
            _quizRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(false);

            var createQuestionCommand = new UpdateQuestionCommand()
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
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _questionCommandHandler.Handle(createQuestionCommand, CancellationToken.None));
            _autoMocker.VerifyAll();
        }

        [Fact]
        public async Task Handle_QuestionIdNotExist_ShouldNotFound()
        {
            //Arrange
            _quizRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(true);
            _questionRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Question>>()))
               .ReturnsAsync(false);

            var createQuestionCommand = new UpdateQuestionCommand()
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
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _questionCommandHandler.Handle(createQuestionCommand, CancellationToken.None));
            _autoMocker.VerifyAll();
        }


        [Fact]
        public async Task Handle_ValidQuestionData_SaveShouldFailed()
        {
            //Arrange
            _quizRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(true);
            _questionRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Question>>()))
               .ReturnsAsync(true);
            _questionRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<Question>()))
                .ReturnsAsync(false);

            var createQuestionCommand = new UpdateQuestionCommand()
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
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _questionCommandHandler.Handle(createQuestionCommand, CancellationToken.None));
            _autoMocker.VerifyAll();
        }
    }
}
