using FluentValidation.TestHelper;
using Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.AcceptFriendship
{
    public class AcceptFriendshipValidatorTests
    {
        private readonly AcceptFriendshipValidator _validator;

        public AcceptFriendshipValidatorTests()
        {
            _validator = new AcceptFriendshipValidator();
        }

        [Fact]
        public void AcceptFriendshipValidator_ShouldHaveErrorWhenUserIdIsNull()
        {
            // Arrange
            var command = new AcceptFriendshipCommand
            {
                UserId = null,
                Username = "TestUser"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Fact]
        public void AcceptFriendshipValidator_ShouldHaveErrorWhenUserIdIsEmpty()
        {
            // Arrange
            var command = new AcceptFriendshipCommand
            {
                UserId = "",
                Username = "TestUser"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Fact]
        public void AcceptFriendshipValidator_ShouldHaveErrorWhenUsernameIsNull()
        {
            // Arrange
            var command = new AcceptFriendshipCommand
            {
                UserId = "123",
                Username = null
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Username);
        }

        [Fact]
        public void AcceptFriendshipValidator_ShouldHaveErrorWhenUsernameIsEmpty()
        {
            // Arrange
            var command = new AcceptFriendshipCommand
            {
                UserId = "123",
                Username = ""
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Username);
        }

        [Fact]
        public void AcceptFriendshipValidator_ShouldNotHaveErrorWhenUserIdAndUsernameArePresent()
        {
            // Arrange
            var command = new AcceptFriendshipCommand
            {
                UserId = "123",
                Username = "TestUser"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
