using Gymby.Application.Mediatr.DiaryDay.Queries.GetDiaryDay;

namespace Gymby.UnitTests.Mediatr.DiaryDay.Queries.GetDiaryDay
{
    public class GetDiaryDayCommandTests
    {
        [Fact]
        public void GetDiaryDayCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var diaryDay = "testDiaryDay";
            var date = DateTime.Now;

            // Act
            var command = new GetDiaryDayCommand
            {
                DiaryId = diaryDay,
                Date = date,
                UserId = userId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(diaryDay, command.DiaryId);
            Assert.Equal(date, command.Date);
        }
    }
}
