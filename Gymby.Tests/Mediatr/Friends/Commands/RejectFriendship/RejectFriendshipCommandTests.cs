using Gymby.Application.Mediatr.Friends.Commands.RejectFriendship;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.RejectFriendship
{
    public class RejectFriendshipCommandTests
    {
        [Fact]
        public void RejectFriendshipCommand_ShouldSetPropertiesAndNotNull()
        {
            // Arrange
            var command = new RejectFriendshipCommand();
            var userId = "testUser";
            var username = "testUser123";

            // Act
            command.UserId = userId;
            command.Username = username;

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(username, command.Username);
            Assert.NotNull(command.UserId);
            Assert.NotNull(command.Username);
        }
    }
}
