using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Profiles.Commands
{
    public class UpdateProfileCommandTests
    {
        [Fact]
        public void UpdateProfileCommand_ShouldSetOptions()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;

            // Act
            var command = new UpdateProfileCommand(options);

            // Assert
            Assert.Same(options, command.Options);
        }

        [Fact]
        public void UpdateProfileCommand_ShouldSetProperties()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var profileId = "profileId";
            var userId = "userId";
            var username = "username";
            var email = "test@example.com";
            var firstName = "John";
            var lastName = "Doe";
            var description = "Profile description";
            var instagramUrl = "instagramUrl";
            var facebookUrl = "facebookUrl";
            var telegramUsername = "telegramUsername";

            // Act
            var command = new UpdateProfileCommand(options)
            {
                ProfileId = profileId,
                UserId = userId,
                Username = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Description = description,
                InstagramUrl = instagramUrl,
                FacebookUrl = facebookUrl,
                TelegramUsername = telegramUsername,
            };

            // Assert
            Assert.Equal(profileId, command.ProfileId);
            Assert.Equal(userId, command.UserId);
            Assert.Equal(username, command.Username);
            Assert.Equal(email, command.Email);
            Assert.Equal(firstName, command.FirstName);
            Assert.Equal(lastName, command.LastName);
            Assert.Equal(description, command.Description);
            Assert.Equal(instagramUrl, command.InstagramUrl);
            Assert.Equal(facebookUrl, command.FacebookUrl);
            Assert.Equal(telegramUsername, command.TelegramUsername);
        }
    }
}
