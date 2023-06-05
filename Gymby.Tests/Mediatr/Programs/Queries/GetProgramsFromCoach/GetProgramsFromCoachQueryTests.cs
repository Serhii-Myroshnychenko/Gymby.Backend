using Gymby.Application.Mediatr.Programs.Queries.GetProgramsFromCoach;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.GetProgramsFromCoach
{
    public class GetProgramsFromCoachQueryTests
    {
        [Fact]
        public void GetProgramsFromCoachQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";

            // Act
            var command = new GetProgramsFromCoachQuery
            {
                UserId = userId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
        }
    }
}
