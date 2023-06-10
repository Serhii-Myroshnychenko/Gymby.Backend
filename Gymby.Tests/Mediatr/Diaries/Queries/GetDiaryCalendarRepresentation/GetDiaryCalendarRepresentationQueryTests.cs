using Gymby.Application.Mediatr.Diaries.Queries.GetDiaryCalendarRepresentaion;

namespace Gymby.UnitTests.Mediatr.Diaries.Queries.GetDiaryCalendarRepresentation
{
    public class GetDiaryCalendarRepresentationQueryTests
    {
        [Fact]
        public void GetDiaryCalendarRepresentationQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var diaryId = "testDiaryId";
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            // Act
            var command = new GetDiaryCalendarRepresentationQuery
            {
                UserId = userId,
                DiaryId = diaryId,
                StartDate = startDate,
                EndDate = endDate
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(diaryId, command.DiaryId);
            Assert.Equal(startDate, command.StartDate);
            Assert.Equal(endDate, command.EndDate);
        }
    }
}
