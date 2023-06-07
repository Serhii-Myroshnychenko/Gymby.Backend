using Gymby.Application.Mediatr.Statistics.Queries.GetApproachesDoneCouneByDate;

namespace Gymby.UnitTests.Mediatr.Statistics.Queries.GetApproachesDoneCountByDateTests
{
    public class GetApproachesDoneCountByDateQueryTests
    {
        [Fact]
        public void GetExercisesDoneCountByDateQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            // Act
            var command = new GetApproachesDoneCountByDateQuery
            {
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(startDate, command.StartDate);
            Assert.Equal(endDate, command.EndDate);
        }
    }
}
