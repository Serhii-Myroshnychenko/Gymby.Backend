using Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Commands.CreateProfile
{
    public class CreateProfileCommandTests
    {
        [Fact]
        public void CreateProfileCommand_ShouldSetPropertiesAndNotNull()
        {
            // Arrange
            var command = new CreateProfileCommand();
            var userId = "testUser";
            var email = "test@example.com";

            // Act
            command.UserId = userId;
            command.Email = email;

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(email, command.Email);
            Assert.NotNull(command.UserId);
            Assert.NotNull(command.Email);
        }
    }
}
