using AppQuiz.Domain;
using AppQuiz.Persistence;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Shared.Persistence.MongoDb;
using Xunit;

namespace AppQuiz.UnitTests.RepositoryTests
{
    public class QuizRepositoryTests
    {
        private Mock<IOptions<ConnectionStrings>> _mockOptions;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;
        public QuizRepositoryTests()
        {
            _mockOptions = new Mock<IOptions<ConnectionStrings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public void QuizDBContext_Constructor_Success()
        {
            //Arrange
            var settings = new ConnectionStrings()
            {
                Mongo = "mongodb://localhost:27017/Quiz"
            };

            var mongodbUrl = new MongoUrl(settings.Mongo);
            
            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
                    .GetDatabase(mongodbUrl.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new QuizDbContext(_mockOptions.Object);

            //Assert 
            Assert.NotNull(context);
        }

        [Fact]
        public void QuizDBContext_GetCollection_NameEmpty_Failure()
        {

            //Arrange
            var settings = new ConnectionStrings()
            {
                Mongo = "mongodb://localhost:27017/Quiz"
            };

            var mongodbUrl = new MongoUrl(settings.Mongo);

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
                    .GetDatabase(mongodbUrl.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act
            var context = new QuizDbContext(_mockOptions.Object);
            var myCollection = context.GetCollection<Quiz>("");
            //Assert
            Assert.Null(myCollection);
        }

        [Fact]
        public void MongoQuizDBContext_GetCollection_ValidName_Success()
        {
            //Arrange
            var settings = new ConnectionStrings()
            {
                Mongo = "mongodb://localhost:27017/Quiz"
            };

            var mongodbUrl = new MongoUrl(settings.Mongo);

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c => c
                    .GetDatabase(mongodbUrl.DatabaseName, null))
                .Returns(_mockDB.Object);

            //Act 
            var context = new QuizDbContext(_mockOptions.Object);
            var myCollection = context.GetCollection<Quiz>("Quiz");

            //Assert 
            Assert.NotNull(myCollection);
        }
    }
}
