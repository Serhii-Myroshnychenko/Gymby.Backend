using Gymby.Application.Mediatr.Programs.Queries.GetFreePrograms;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.GetFreePrograms
{
    public class GetFreeProgramsQueryTests
    {
        [Fact]
        public void GetFreeProgramsQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";

            // Act
            var command = new GetFreeProgramsQuery
            {
                UserId = userId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
        }
    }
}
