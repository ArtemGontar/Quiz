using AppQuiz.Application.Chapters.Commands.Update;
using AppQuiz.Application.Infrastructure;
using AppQuiz.Domain;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using Shared.Common;
using Shared.Persistence.MongoDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AppQuiz.UnitTests.ChapterTests
{
    public class UpdateChapterCommandHandlerTests
    {
        private AutoMocker _autoMocker;
        private UpdateChapterCommandHandler _chapterCommandHandler;
        private Mock<IRepository<Chapter>> _chapterRepositoryMock;
        public UpdateChapterCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();

            _autoMocker.Use<IMapper>(new MapperConfiguration(x => x.AddMaps(typeof(QuizProfile).Assembly)).CreateMapper());
            _chapterCommandHandler = _autoMocker.CreateInstance<UpdateChapterCommandHandler>();
            _chapterRepositoryMock = _autoMocker.GetMock<IRepository<Chapter>>();
        }

        [Fact]
        public async Task Handle_ValidChapterData_ShouldSuccess()
        {
            //Arrange
            var chapterId = Guid.NewGuid();
            var command = new UpdateChapterCommand()
            {
                Name = "anonymousName",
                EnglishLevel = EnglishLevel.Beginner
            };
            command.SetId(chapterId);

            _chapterRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
                .ReturnsAsync(true);
            _chapterRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<Chapter>()))
                .ReturnsAsync(true);
            //Act

            var result = await _chapterCommandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(chapterId, result);
            _autoMocker.VerifyAll();
        }

        [Fact]
        public async Task Handle_ValidChapterData_ChapterShouldNotFound()
        {

            //Arrange
            var chapterId = Guid.NewGuid();
            var command = new UpdateChapterCommand()
            {
                Name = "anonymousName",
                EnglishLevel = EnglishLevel.Beginner
            };
            command.SetId(chapterId);

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
            var chapterId = Guid.NewGuid();
            //Arrange
            var command = new UpdateChapterCommand()
            {
                Name = "anonymousName",
                EnglishLevel = EnglishLevel.Beginner
            };
            command.SetId(chapterId);

            _chapterRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
                .ReturnsAsync(true);
            _chapterRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<Chapter>()))
                .ReturnsAsync(false);

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _chapterCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }
    }
}
