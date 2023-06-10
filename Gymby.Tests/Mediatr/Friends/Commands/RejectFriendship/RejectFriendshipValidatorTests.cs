using FluentValidation.TestHelper;
using Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;
using Gymby.Application.Mediatr.Friends.Commands.RejectFriendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.RejectFriendship
{
    public class RejectFriendshipValidatorTests
    {
        private readonly RejectFriendshipValidator _validator;

        public RejectFriendshipValidatorTests()
        {
            _validator = new RejectFriendshipValidator();
        }

        [Fact]
        public void RejectFriendshipValidator_ShouldHaveErrorWhenUserIdIsNull()
        {
            // Arrange
            var command = new RejectFriendshipCommand
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
        public void RejectFriendshipValidator_ShouldHaveErrorWhenUserIdIsEmpty()
        {
            // Arrange
            var command = new RejectFriendshipCommand
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
        public void RejectFriendshipValidator_ShouldHaveErrorWhenUsernameIsNull()
        {
            // Arrange
            var command = new RejectFriendshipCommand
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
        public void RejectFriendshipValidator_ShouldHaveErrorWhenUsernameIsEmpty()
        {
            // Arrange
            var command = new RejectFriendshipCommand
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
        public void RejectFriendshipValidator_ShouldNotHaveErrorWhenUserIdAndUsernameArePresent()
        {
            // Arrange
            var command = new RejectFriendshipCommand
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
