using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.GetMyProfileTests
{
    public class GetMyProfileValidatorTests
    {
        [Fact]
        public async Task GetMyProfileValidator_ShouldHaveErrorWhenUserIdIsNull()
        {
            // Arrange
            var validator = new GetMyProfileValidator();
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var query = new GetMyProfileQuery(appConfigOptions)
            {
                UserId = null
            };

            // Act
            var result = await validator.ValidateAsync(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(query.UserId));
        }

        [Fact]
        public async Task GetMyProfileValidator_ShouldHaveErrorWhenUserIdIsEmpty()
        {
            // Arrange
            var validator = new GetMyProfileValidator();
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var query = new GetMyProfileQuery(appConfigOptions)
            {
                UserId = string.Empty
            };

            // Act
            var result = await validator.ValidateAsync(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(query.UserId));
        }

        [Fact]
        public async Task GetMyProfileValidator_ShouldNotHaveErrorWhenUserIdIsProvided()
        {
            // Arrange
            var validator = new GetMyProfileValidator();
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);
            var query = new GetMyProfileQuery(appConfigOptions)
            {
                UserId = "userId"
            };

            // Act
            var result = await validator.ValidateAsync(query);

            // Assert
            Assert.True(result.IsValid);
            Assert.DoesNotContain(result.Errors, error => error.PropertyName == nameof(query.UserId));
        }
    }
}
