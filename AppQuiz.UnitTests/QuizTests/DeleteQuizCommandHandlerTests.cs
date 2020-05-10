using AppQuiz.Application.Infrastructure;
using AppQuiz.Application.Quizzes.Commands.Delete;
using AppQuiz.Domain;
using AutoMapper;
using MassTransit;
using Moq;
using Moq.AutoMock;
using Shared.Bus.Messages.Messages;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AppQuiz.UnitTests.QuizTests
{
    public class DeleteQuizCommandHandlerTests
    {
        private AutoMocker _autoMocker;
        private DeleteQuizCommandHandler _quizCommandHandler;
        private Mock<IRepository<Quiz>> _quizRepositoryMock;
        private Mock<ISendEndpointProvider> _sendEndpointProviderMock;
        private readonly Mock<ISendEndpoint> _sendEndpointMock;

        public DeleteQuizCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();

            EndpointConvention.Map<DeleteQuizMessage>(new Uri("http://random"));
            _autoMocker.Use<IMapper>(new MapperConfiguration(x => x.AddMaps(typeof(QuizProfile).Assembly)).CreateMapper());
            _quizCommandHandler = _autoMocker.CreateInstance<DeleteQuizCommandHandler>();
            _sendEndpointMock = new Mock<ISendEndpoint>();
            _quizRepositoryMock = _autoMocker.GetMock<IRepository<Quiz>>();

        }

        [Fact]
        public async Task Handle_ValidQuizData_ShouldSuccess()
        {
            //Arrange
            var quizId = Guid.NewGuid();
            var command = new DeleteQuizCommand(quizId);

            _quizRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(true);
            _quizRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(true);

            _sendEndpointProviderMock = _autoMocker.GetMock<ISendEndpointProvider>();
            _sendEndpointProviderMock.Setup(x => x.GetSendEndpoint(It.IsAny<Uri>()))
                .Returns(Task.FromResult(_sendEndpointMock.Object));
            
            var result = await _quizCommandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result);
            _autoMocker.VerifyAll();
        }

        [Fact]
        public async Task Handle_ValidQuizData_ChapterShouldNotFound()
        {

            //Arrange
            var quizId = Guid.NewGuid();
            var command = new DeleteQuizCommand(quizId);

            _quizRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(false);

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _quizCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }


        [Fact]
        public async Task Handle_ValidChapterData_SaveShouldFailed()
        {
            //Arrange
            var quizId = Guid.NewGuid();
            var command = new DeleteQuizCommand(quizId);

            _quizRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(true);
            _quizRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(false);

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _quizCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }
    }
}
