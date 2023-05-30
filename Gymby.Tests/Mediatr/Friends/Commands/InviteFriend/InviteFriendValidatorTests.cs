using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.InviteFriend
{
    public class InviteFriendValidatorTests
    {
        private readonly InviteFriendValidator _validator;

        public InviteFriendValidatorTests()
        {
            _validator = new InviteFriendValidator();
        }

        [Fact]
        public void InviteFriendValidator_ShouldPassValidation()
        {
            // Arrange
            var command = new InviteFriendCommand
            {
                UserId = "12345",
                Username = "john_doe"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(null, "john_doe")]
        [InlineData("12345", null)]
        [InlineData(null, null)]
        public void InviteFriendValidator_MissingUserIdOrUsername_ShouldFailValidation(string userId, string username)
        {
            // Arrange
            var command = new InviteFriendCommand
            {
                UserId = userId,
                Username = username
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            if (userId == null)
                Assert.Contains(result.Errors, error => error.PropertyName == "UserId");
            if (username == null)
                Assert.Contains(result.Errors, error => error.PropertyName == "Username");
        }

        [Theory]
        [InlineData("", "john_doe")]
        [InlineData("12345", "")]
        [InlineData("   ", "   ")]
        public void InviteFriendValidator_EmptyOrWhitespaceUserIdOrUsername_ShouldFailValidation(string userId, string username)
        {
            // Arrange
            var command = new InviteFriendCommand
            {
                UserId = userId,
                Username = username
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            if (userId == string.Empty || userId.Trim().Length == 0)
                Assert.Contains(result.Errors, error => error.PropertyName == "UserId");
            if (username == string.Empty || username.Trim().Length == 0)
                Assert.Contains(result.Errors, error => error.PropertyName == "Username");
        }
    }
}
