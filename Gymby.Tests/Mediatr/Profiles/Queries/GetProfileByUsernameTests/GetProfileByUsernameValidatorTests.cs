using Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.GetProfileByUsernameTests
{
    public class GetProfileByUsernameValidatorTests
    {
        [Fact]
        public void GetProfileByUsernameValidator_ShouldHaveErrorWhenUsernameIsNull()
        {
            // Arrange
            var validator = new GetProfileByUsernameValidator();
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var query = new GetProfileByUsernameQuery(appConfigOptions)
            {
                Username = null
            };

            // Act
            var result = validator.Validate(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(query.Username));
        }

        [Fact]
        public void GetProfileByUsernameValidator_ShouldHaveErrorWhenUsernameIsEmpty()
        {
            // Arrange
            var validator = new GetProfileByUsernameValidator();
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var query = new GetProfileByUsernameQuery(appConfigOptions)
            {
                Username = string.Empty
            };

            // Act
            var result = validator.Validate(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(query.Username));
        }

        [Fact]
        public void GetProfileByUsernameValidator_ShouldNotHaveErrorWhenUsernameIsProvided()
        {
            // Arrange
            var validator = new GetProfileByUsernameValidator();
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var query = new GetProfileByUsernameQuery(appConfigOptions)
            {
                Username = "testuser"
            };

            // Act
            var result = validator.Validate(query);

            // Assert
            Assert.True(result.IsValid);
            Assert.DoesNotContain(result.Errors, error => error.PropertyName == nameof(query.Username));
        }
    }
}
