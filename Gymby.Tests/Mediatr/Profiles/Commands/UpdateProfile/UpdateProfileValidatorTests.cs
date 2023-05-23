using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Profiles.Commands.UpdateProfile
{
    public class UpdateProfileValidatorTests
    {
        private readonly UpdateProfileValidator _validator;

        public UpdateProfileValidatorTests()
        {
            _validator = new UpdateProfileValidator();
        }

        [Fact]
        public async Task UpdateProfileValidator_ShouldValidateCorrectCommand()
        {
            // Arrange
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var command = new UpdateProfileCommand(appConfigOptions)
            {
                ProfileId = "profileId",
                UserId = "userId",
                Email = "test@example.com"
            };

            // Act
            var validationResult = await _validator.ValidateAsync(command);

            // Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public async Task UpdateProfileValidator_ShouldFailValidationWhenProfileIdIsNull()
        {
            // Arrange
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var command = new UpdateProfileCommand(appConfigOptions)
            {
                ProfileId = null,
                UserId = "userId",
                Email = "test@example.com"
            };

            // Act
            var validationResult = await _validator.ValidateAsync(command);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == nameof(command.ProfileId));
        }

        [Fact]
        public async Task UpdateProfileValidator_ShouldFailValidationWhenUserIdIsEmpty()
        {
            // Arrange
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var command = new UpdateProfileCommand(appConfigOptions)
            {
                ProfileId = "profileId",
                UserId = string.Empty,
                Email = "test@example.com"
            };

            // Act
            var validationResult = await _validator.ValidateAsync(command);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == nameof(command.UserId));
        }

        [Fact]
        public async Task UpdateProfileValidator_ShouldFailValidationWhenEmailIsNull()
        {
            // Arrange
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var command = new UpdateProfileCommand(appConfigOptions)
            {
                ProfileId = "profileId",
                UserId = "userId",
                Email = null
            };

            // Act
            var validationResult = await _validator.ValidateAsync(command);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == nameof(command.Email));
        }
    }
}
