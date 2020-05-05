using AppQuiz.Application.Chapters.Commands.Create;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;

namespace AppQuiz.UnitTests.ChapterTests
{
    using AppQuiz.Application.Infrastructure;
    using AppQuiz.Domain;
    using AutoMapper;
    using Moq;
    using Shared.Common;
    using Shared.Persistence.MongoDb;
    using System.Threading;
    using Xunit;

    public class CreateChapterCommandHandlerTests
    {
        private AutoMocker _autoMocker;
        private CreateChapterCommandHandler _chapterCommandHandler;
        private Mock<IRepository<Chapter>> _chapterRepository;

        public CreateChapterCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();

            _autoMocker.Use<IMapper>(new MapperConfiguration(x => x.AddMaps(typeof(QuizProfile).Assembly)).CreateMapper());
            _chapterCommandHandler = _autoMocker.CreateInstance<CreateChapterCommandHandler>();

            _chapterRepository = _autoMocker.GetMock<IRepository<Chapter>>();
        }

        [Fact]
        public async Task Handle_ValidChapterData_ShouldSuccess()
        {
            //Arrange
            var command = new CreateChapterCommand()
            {
                Name = "anonymousName",
                EnglishLevel = EnglishLevel.Beginner
            };

            _chapterRepository.Setup(x => x.SaveAsync(It.IsAny<Chapter>()))
                .Callback<Chapter>(x => x.Id = Guid.NewGuid())
                .ReturnsAsync(true);
            //Act

            var result = await _chapterCommandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotEqual(Guid.Empty, result);
            _autoMocker.VerifyAll();
        }

        [Fact]
        public async Task Handle_ValidChapterData_SaveShouldFailed()
        {
            //Arrange
            var command = new CreateChapterCommand()
            {
                Name = "anonymousName",
                EnglishLevel = EnglishLevel.Beginner
            };

            _chapterRepository.Setup(x => x.SaveAsync(It.IsAny<Chapter>()))
                .ReturnsAsync(false);
            
            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _chapterCommandHandler.Handle(command, CancellationToken.None)); 
            _autoMocker.VerifyAll();
        }
    }
}
