using Gymby.Application.Mediatr.Programs.Queries.QueryProgram;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.QueryProgram
{
    public class QueryProgramQueryTests
    {
        [Fact]
        public void QueryProgramQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var query = "testQuery";
            var type = "testType";
            var level = "testLevel";
            var userId = "testUserId";

            // Act
            var command = new QueryProgramQuery
            {
                Query = query,
                Type = type,
                UserId = userId,
                Level = level
            };

            // Assert
            Assert.Equal(query, command.Query);
            Assert.Equal(type, command.Type);
            Assert.Equal(level, command.Level);
            Assert.Equal(userId, command.UserId);
        }
    }
}
