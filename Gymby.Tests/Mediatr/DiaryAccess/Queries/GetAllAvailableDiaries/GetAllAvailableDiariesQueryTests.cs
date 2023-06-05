using Gymby.Application.Mediatr.DiaryAccesses.Queries.GetAllAvailableDiaries;

namespace Gymby.UnitTests.Mediatr.DiaryAccess.Queries.GetAllAvailableDiaries
{
    public class GetAllAvailableDiariesQueryTests
    {
        [Fact]
        public void GetAllAvailableDiariesQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";

            // Act
            var command = new GetAllAvailableDiariesQuery
            {
                UserId = userId,
            };

            // Assert
            Assert.Equal(userId, command.UserId);
        }
    }
}
