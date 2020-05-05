using AppQuiz.Application.Infrastructure;
using AppQuiz.Application.Quizzes.Commands.Create;
using AppQuiz.Application.Quizzes.Commands.Update;
using AppQuiz.Domain;
using AutoMapper;
using MediatR;
using Moq;
using Moq.AutoMock;
using Shared.Common;
using Shared.Persistence.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AppQuiz.UnitTests.QuizTests
{
    public class UpdateQuizCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private UpdateQuizCommandHandler _quizCommandHandler;
        private Mock<IRepository<Quiz>> _quizRepository;
        private Mock<IRepository<Chapter>> _chapterRepository;
        private Mock<IMediator> _mediator;

        public UpdateQuizCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _autoMocker.Use<IMapper>(new MapperConfiguration(x => x.AddMaps(typeof(QuizProfile).Assembly)).CreateMapper());
            _quizCommandHandler = _autoMocker.CreateInstance<UpdateQuizCommandHandler>();
            _quizRepository = _autoMocker.GetMock<IRepository<Quiz>>();
            _chapterRepository = _autoMocker.GetMock<IRepository<Chapter>>();
            _mediator = _autoMocker.GetMock<IMediator>();
        }

        [Fact]
        public async Task Handle_ValidQuizData_ShouldSuccess()
        {
            //Arrange
            var quizId = Guid.NewGuid();
            _chapterRepository.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
               .ReturnsAsync(true);
            _quizRepository.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
               .ReturnsAsync(true);
            _quizRepository.Setup(x => x.SaveAsync(It.IsAny<Quiz>()))
                .Callback<Quiz>(x => x.Id = quizId)
                .ReturnsAsync(true);
            _mediator.Setup(x => x.Send(It.IsAny<UpdateQuizCommandHandler>(), It.IsAny<CancellationToken>()));

            var command = new UpdateQuizCommand
            {
                Title = "anonymousQuizTitle",
                Priority = Priority.Low,
                ChapterId = Guid.NewGuid(),
                Questions = new List<Question>() {
                    new Question()
                    {
                        Title = "anonymousQuestionTitle",
                        CorrectAnswer = "anonymousCorrectAnswer",
                        Options = new List<Option>()
                        {
                            new Option()
                            {
                                Value = "anonymousValue"
                            }
                        }
                    }
                }
            };
            command.SetIdAndOwnerId(quizId, Guid.NewGuid());

            //Act
            var result = await _quizCommandHandler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(quizId, result);
        }

        [Fact]
        public async Task Handle_ChapterNotExist_ChapterShouldNotFound()
        {

            //Arrange
            _chapterRepository.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
               .ReturnsAsync(false);

            var command = new UpdateQuizCommand
            {
                Title = "anonymousQuizTitle",
                Priority = Priority.Low,
                ChapterId = Guid.NewGuid(),
                Questions = new List<Question>() {
                    new Question()
                    {
                        Title = "anonymousQuestionTitle",
                        CorrectAnswer = "anonymousCorrectAnswer",
                        Options = new List<Option>()
                        {
                            new Option()
                            {
                                Value = "anonymousValue"
                            }
                        }
                    }
                }
            };
            command.SetIdAndOwnerId(Guid.NewGuid(), Guid.NewGuid());

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _quizCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }

        [Fact]
        public async Task Handle_QuizNotExist_QuizShouldNotFound()
        {

            //Arrange
            _chapterRepository.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
               .ReturnsAsync(true);
            _quizRepository.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
                .ReturnsAsync(false);

            var command = new UpdateQuizCommand
            {
                Title = "anonymousQuizTitle",
                Priority = Priority.Low,
                ChapterId = Guid.NewGuid(),
                Questions = new List<Question>() {
                    new Question()
                    {
                        Title = "anonymousQuestionTitle",
                        CorrectAnswer = "anonymousCorrectAnswer",
                        Options = new List<Option>()
                        {
                            new Option()
                            {
                                Value = "anonymousValue"
                            }
                        }
                    }
                }
            };
            command.SetIdAndOwnerId(Guid.NewGuid(), Guid.NewGuid());

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _quizCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }

        [Fact]
        public async Task Handle_ValidQuizData_SaveShouldFailed()
        {
            //Arrange
            _chapterRepository.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Chapter>>()))
               .ReturnsAsync(true);
            _quizRepository.Setup(x => x.AnyAsync(It.IsAny<ISpecification<Quiz>>()))
               .ReturnsAsync(true);
            _quizRepository.Setup(x => x.SaveAsync(It.IsAny<Quiz>()))
                .ReturnsAsync(false);

            var command = new UpdateQuizCommand
            {
                Title = "anonymousQuizTitle",
                Priority = Priority.Low,
                Questions = new List<Question>() {
                    new Question()
                    {
                        Title = "anonymousQuestionTitle",
                        CorrectAnswer = "anonymousCorrectAnswer",
                        Options = new List<Option>()
                        {
                            new Option()
                            {
                                Value = "anonymousValue"
                            }
                        }
                    }
                }
            };
            command.SetIdAndOwnerId(Guid.NewGuid(), Guid.NewGuid());

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _quizCommandHandler.Handle(command, CancellationToken.None));
            _autoMocker.VerifyAll();
        }
    }
}
