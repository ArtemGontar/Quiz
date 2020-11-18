using AppQuiz.Application.Chapters.Commands.Delete;
using AppQuiz.Application.Infrastructure;
using AppQuiz.Domain;
using AutoMapper;
using MassTransit;
using Moq;
using Moq.AutoMock;
using Shared.Bus.Messages;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AppQuiz.UnitTests.ChapterTests
{
    public class DeleteChapterCommandHandlerTests
    {
        private AutoMocker _autoMocker;
        private DeleteChapterCommandHandler _chapterCommandHandler;
        private Mock<IRepository<Chapter>> _chapterRepositoryMock;
        private Mock<ISendEndpointProvider> _sendEndpointProviderMock;
        private readonly Mock<ISendEndpoint> _sendEndpointMock;

        public DeleteChapterCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();

            EndpointConvention.Map<DeleteChapterMessage>(new Uri("http://random"));
            _autoMocker.Use<IMapper>(new MapperConfiguration(x => x.AddMaps(typeof(QuizProfile).Assembly)).CreateMapper());
            _chapterCommandHandler = _autoMocker.CreateInstance<DeleteChapterCommandHandler>();
            _sendEndpointMock = new Mock<ISendEndpoint>();
            _chapterRepositoryMock = _autoMocker.GetMock<IRepository<Chapter>>();
            
        }

        [Fact]
        public async Task Handle_ValidChapterData_ShouldSuccess()
        {
            //Arrange
            var chapterId = Guid.NewGuid();
            var command = new DeleteChapterCommand(chapterId);

            _chapterRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
                .ReturnsAsync(true);
            _chapterRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ISpecification<Chapter>>()))
                .ReturnsAsync(true);

            _sendEndpointProviderMock = _autoMocker.GetMock<ISendEndpointProvider>();
            _sendEndpointProviderMock.Setup(x => x.GetSendEndpoint(It.IsAny<Uri>()))
                .Returns(Task.FromResult(_sendEndpointMock.Object));
            //TODO: How to mock?
            //_sendEndpointProviderMock.Setup(x => x.Send(It.IsAny<DeleteChapterMessage>(), It.IsAny<CancellationToken>()));
            //Act

            var result = await _chapterCommandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result);
            _autoMocker.VerifyAll();
        }

        [Fact]
        public async Task Handle_ValidChapterData_ChapterShouldNotFound()
        {

            //Arrange
            var chapterId = Guid.NewGuid();
            var command = new DeleteChapterCommand(chapterId);

            _chapterRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
                .ReturnsAsync(false);

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _chapterCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }


        [Fact]
        public async Task Handle_ValidChapterData_SaveShouldFailed()
        {
            //Arrange
            var chapterId = Guid.NewGuid();
            var command = new DeleteChapterCommand(chapterId);

            _chapterRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
                .ReturnsAsync(true);
            _chapterRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<ISpecification<Chapter>>()))
                .ReturnsAsync(false);

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _chapterCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }
    }
}
