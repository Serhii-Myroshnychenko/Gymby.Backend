using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.InviteFriend
{
    public class InviteFriendCommandTests
    {
        [Fact]
        public void InviteFriendCommand_ShouldSetPropertiesAndNotNull()
        {
            // Arrange
            var command = new InviteFriendCommand();
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
