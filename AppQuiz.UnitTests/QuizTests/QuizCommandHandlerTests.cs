using AppQuiz.Application.Quizzes.Commands.Create;
using AppQuiz.Application.Quizzes.Commands.Delete;
using AppQuiz.Application.Quizzes.Commands.Update;
using AppQuiz.Application.Quizzes.Queries.GetAll;
using AppQuiz.Application.Quizzes.Queries.GetById;
using AppQuiz.Domain;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Persistence.MongoDb;
using Xunit;

namespace AppQuiz.UnitTests.QuizTests
{
    public class QuizCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private Mock<IRepository<Quiz>> _quizRepository;
        public QuizCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact]
        public async Task CreateCommandHandler_HandleSuccess()
        {
            //Arrange
            var createQuizCommandHandler = _autoMocker.CreateInstance<CreateQuizCommandHandler>();
            _quizRepository = _autoMocker.GetMock<IRepository<Quiz>>();
            _quizRepository.Setup(x => x.SaveAsync(It.IsAny<Quiz>()))
                .ReturnsAsync(true);
            var createQuizCommand = new CreateQuizCommand
            {
                Title = "anonymousTitle"
            };
            

            //Act
            var result = await createQuizCommandHandler.Handle(createQuizCommand, CancellationToken.None);

            //Assert
            Assert.NotEqual(Guid.NewGuid(), result);
        }

        [Fact]
        public async Task UpdateCommandHandler_HandleSuccess()
        {
            //Arrange
            var updateQuizCommandHandler = _autoMocker.CreateInstance<UpdateQuizCommandHandler>();
            _quizRepository = _autoMocker.GetMock<IRepository<Quiz>>();
            _quizRepository.Setup(x => x.SaveAsync(It.IsAny<Quiz>()))
                .ReturnsAsync(true);
            var updateQuizCommand = new UpdateQuizCommand()
            {
                Title = "anonymousTitle",
            };
            updateQuizCommand.SetIdAndOwnerId(Guid.NewGuid(), Guid.NewGuid());

            //Act
            var result = await updateQuizCommandHandler.Handle(updateQuizCommand, CancellationToken.None);

            //Assert
            Assert.NotEqual(Guid.NewGuid(), result);
        }

        [Fact]
        public async Task DeleteCommandHandler_HandleSuccess()
        {
            //Arrange
            var deleteQuizCommandHandler = _autoMocker.CreateInstance<DeleteQuizCommandHandler>();
            _quizRepository = _autoMocker.GetMock<IRepository<Quiz>>();
            _quizRepository.Setup(x => x.DeleteAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(true);
            var deleteQuizCommand = new DeleteQuizCommand(Guid.NewGuid());

            //Act
            var result = await deleteQuizCommandHandler.Handle(deleteQuizCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllQuizQueryHandler_HandleSuccess()
        {
            //Arrange
            var getAllQuizQueryHandler = _autoMocker.CreateInstance<GetAllQuizQueryHandler>();
            _quizRepository = _autoMocker.GetMock<IRepository<Quiz>>();
            _quizRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Quiz>());
            var getAllQuizQuery = new GetAllQuizQuery();


            //Act
            var result = await getAllQuizQueryHandler.Handle(getAllQuizQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetQuizByIdQueryHandler_HandleSuccess()
        {
            //Arrange
            var getQuizByIdQueryHandler = _autoMocker.CreateInstance<GetQuizByIdQueryHandler>();
            _quizRepository = _autoMocker.GetMock<IRepository<Quiz>>();
            _quizRepository.Setup(x => x.GetAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(new Quiz());
            var getAllQuizQuery = new GetQuizByIdQuery()
            {
                QuizId = Guid.NewGuid()
            };

            //Act
            var result = await getQuizByIdQueryHandler.Handle(getAllQuizQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
        }
    }
}
