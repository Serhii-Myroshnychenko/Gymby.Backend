using Gymby.Application.Mediatr.Statistics.Queries.GetExercisesDoneCountByDate;

namespace Gymby.UnitTests.Mediatr.Statistics.Queries.GetExercisesDoneCountByDateTests
{
    public class GetExercisesDoneCountByDateQueryTests
    {
        [Fact]
        public void GetExercisesDoneCountByDateQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            // Act
            var command = new GetExercisesDoneCountByDateQuery
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
