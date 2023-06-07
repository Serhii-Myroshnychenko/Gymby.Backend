using Gymby.Application.Mediatr.Statistics.Queries.GetAllNumberStatistics;

namespace Gymby.UnitTests.Mediatr.Statistics.Queries.GetAllNumberStatisticsTests
{
    public class GetAllNumberStatisticsQueryTests
    {
        [Fact]
        public void GetAllNumberStatisticsQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";

            // Act
            var command = new GetAllNumberStatisticsQuery
            {
                UserId = userId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
        }
    }
}
