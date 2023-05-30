using Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.AcceptFriendship
{
    public class AcceptFriendshipCommandTests
    {
        [Fact]
        public void AcceptFriendshipCommand_ShouldSetPropertiesAndNotNull()
        {
            // Arrange
            var command = new AcceptFriendshipCommand();
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
