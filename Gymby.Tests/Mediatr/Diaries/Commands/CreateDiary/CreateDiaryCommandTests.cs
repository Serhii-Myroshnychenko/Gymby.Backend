using Gymby.Application.Mediatr.Diaries.Command.CreateDiary;

namespace Gymby.UnitTests.Mediatr.Diaries.Commands.CreateDiary
{
    public class CreateDiaryCommandTests
    {
        [Fact]
        public void CreateDiaryCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";

            // Act
            var command = new CreateDiaryCommand
            {
                UserId = userId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
        }
    }
}
