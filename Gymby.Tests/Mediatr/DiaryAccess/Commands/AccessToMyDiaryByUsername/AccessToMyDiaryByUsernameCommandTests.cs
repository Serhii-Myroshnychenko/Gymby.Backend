using Gymby.Application.Mediatr.DiaryAccesses.Commands.AccessToMyDiaryByUsername;

namespace Gymby.UnitTests.Mediatr.DiaryAccess.Commands.AccessToMyDiaryByUsername
{
    public class AccessToMyDiaryByUsernameCommandTests
    {
        [Fact]
        public void AccessToMyDiaryByUsernameCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var username = "testUsername";

            // Act
            var command = new AccessToMyDiaryByUsernameCommand
            {
                UserId = userId,
                Username = username
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(username, command.Username);
        }
    }
}
