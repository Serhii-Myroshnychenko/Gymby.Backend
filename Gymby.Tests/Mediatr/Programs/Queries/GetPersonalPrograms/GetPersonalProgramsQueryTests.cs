using Gymby.Application.Mediatr.Programs.Queries.GetPersonalPrograms;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.GetPersonalPrograms
{
    public class GetPersonalProgramsQueryTests
    {
        [Fact]
        public void GetPersonalProgramsQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";

            // Act
            var command = new GetPersonalProgramsQuery
            {
                UserId = userId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
        }
    }
}
