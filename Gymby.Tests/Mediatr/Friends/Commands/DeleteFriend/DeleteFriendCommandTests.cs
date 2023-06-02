using Gymby.Application.Mediatr.Friends.Commands.DeleteFriend;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.DeleteFriend
{
    public class DeleteFriendCommandTests
    {
        [Fact]
        public void DeleteFriendCommand_ShouldSetPropertiesAndNotNull()
        {
            // Arrange
            var command = new DeleteFriendCommand();
            var userId = "testUser";
            var username = "testUser123";
            var options = new Mock<IOptions<AppConfig>>().Object;

            // Act
            command.UserId = userId;
            command.Username = username;
            command.Options = options;

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(username, command.Username);
            Assert.Equal(options, command.Options);
            Assert.NotNull(command.UserId);
            Assert.NotNull(command.Username);
        }
    }
}
