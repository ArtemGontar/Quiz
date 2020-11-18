using AppQuiz.Application.Infrastructure;
using AppQuiz.Application.Questions.Commands.Delete;
using AppQuiz.Domain;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Shared.Persistence.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AppQuiz.UnitTests.QuestionTests
{
    public class DeleteQuestionCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private Mock<IRepository<Question>> _questionRepositoryMock;
        private DeleteQuestionCommandHandler _questionCommandHandler;
        public DeleteQuestionCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _autoMocker.Use<IMapper>(new MapperConfiguration(x => x.AddMaps(typeof(QuizProfile).Assembly)).CreateMapper());
            _questionCommandHandler = _autoMocker.CreateInstance<DeleteQuestionCommandHandler>();
            _questionRepositoryMock = _autoMocker.GetMock<IRepository<Question>>();
        }

        [Fact]
        public async Task Handle_ValidChapterData_ShouldSuccess()
        {
            //Arrange
            var quizId = Guid.NewGuid();
            var command = new DeleteQuestionCommand(quizId);

            _questionRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Question>>()))
                .ReturnsAsync(true);
            _questionRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ISpecification<Question>>()))
                .ReturnsAsync(true);

            var result = await _questionCommandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result);
            _autoMocker.VerifyAll();
        }

        [Fact]
        public async Task Handle_ValidChapterData_ChapterShouldNotFound()
        {

            //Arrange
            var quizId = Guid.NewGuid();
            var command = new DeleteQuestionCommand(quizId);

            _questionRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Question>>()))
                .ReturnsAsync(false);

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _questionCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }


        [Fact]
        public async Task Handle_ValidChapterData_SaveShouldFailed()
        {
            //Arrange
            var quizId = Guid.NewGuid();
            var command = new DeleteQuestionCommand(quizId);

            _questionRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Question>>()))
                .ReturnsAsync(true);
            _questionRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ISpecification<Question>>()))
                .ReturnsAsync(false);

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _questionCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }
    }
}
