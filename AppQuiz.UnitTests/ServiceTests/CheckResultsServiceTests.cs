using AppQuiz.Application.Services;
using AppQuiz.Domain;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AppQuiz.UnitTests.ServiceTests
{
    public class CheckResultsServiceTests
    {
        private AutoMocker _autoMocker;
        private readonly CheckResultsService _checkResultService;

        public CheckResultsServiceTests()
        {
            _autoMocker = new AutoMocker();

            _checkResultService = _autoMocker.CreateInstance<CheckResultsService>();
        }



        [Theory]
        [InlineData()]
        public void CheckResult_AllAnswersRight_ShouldSuccess()
        {
            //Arrange
            var correctAnswers = new List<string>() {
                "anonymousAnswer1",
                "anonymousAnswer2",
                "anonymousAnswer3",
            };
            var answers = new List<string>() {
                "anonymousAnswer1",
                "anonymousAnswer2",
                "anonymousAnswer3",
            };

            //Act
            var result = _checkResultService.CheckResult(correctAnswers, answers);

            //Assert
            Assert.Equal(3, result.QuestionsCount);
            Assert.Equal(3, result.CorrectAnswersCount);
            Assert.Equal(0, result.WrongAnswersCount);
            Assert.Equal(1, result.CorrectPercent);
        }

        [Fact]
        public void CheckResult_TwoAnswersRight_ShouldSuccess()
        {
            //Arrange
            var correctAnswers = new List<string>() {
                "anonymousAnswer1",
                "anonymousAnswer2",
                "anonymousAnswer3",
            };
            var answers = new List<string>() {
                "anonymousWrongAnswer1",
                "anonymousAnswer2",
                "anonymousAnswer3",
            };

            //Act
            var result = _checkResultService.CheckResult(correctAnswers, answers);

            //Assert
            Assert.Equal(3, result.QuestionsCount);
            Assert.Equal(2, result.CorrectAnswersCount);
            Assert.Equal(1, result.WrongAnswersCount);
            Assert.Equal("0.67", result.CorrectPercent.ToString("0.00"));
        }

        [Fact]
        public void CheckResult_OneAnswersRight_ShouldSuccess()
        {
            //Arrange
            var correctAnswers = new List<string>() {
                "anonymousAnswer1",
                "anonymousAnswer2",
                "anonymousAnswer3",
            };
            var answers = new List<string>() {
                "anonymousWrongAnswer1",
                "anonymousAnswer2",
                "anonymousWrongAnswer3",
            };

            //Act
            var result = _checkResultService.CheckResult(correctAnswers, answers);

            //Assert
            Assert.Equal(3, result.QuestionsCount);
            Assert.Equal(1, result.CorrectAnswersCount);
            Assert.Equal(2, result.WrongAnswersCount);
            Assert.Equal("0.33", result.CorrectPercent.ToString("0.00"));
        }

        [Fact]
        public void CheckResult_NoneAnswersRight_ShouldSuccess()
        {
            //Arrange
            var correctAnswers = new List<string>() {
                "anonymousAnswer1",
                "anonymousAnswer2",
                "anonymousAnswer3",
            };
            var answers = new List<string>() {
                "anonymousWrongAnswer1",
                "anonymousWrongAnswer2",
                "anonymousWrongAnswer3",
            };

            //Act
            var result = _checkResultService.CheckResult(correctAnswers, answers);

            //Assert
            Assert.Equal(3, result.QuestionsCount);
            Assert.Equal(0, result.CorrectAnswersCount);
            Assert.Equal(3, result.WrongAnswersCount);
            Assert.Equal("0.00", result.CorrectPercent.ToString("0.00"));
        }
    }
}
