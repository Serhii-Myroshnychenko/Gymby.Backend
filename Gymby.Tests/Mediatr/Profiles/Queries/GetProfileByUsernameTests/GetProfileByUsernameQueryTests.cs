using Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.GetProfileByUsernameTests
{
    public class GetProfileByUsernameQueryTests
    {
        [Fact]
        public void GetProfileByUsernameQuery_ShouldBeSetOptions()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;

            // Act
            var query = new GetProfileByUsernameQuery(options);

            // Assert
            Assert.Same(options, query.Options);
        }

        [Fact]
        public void GetProfileByUsernameQuery_ShouldBeSetUsername()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var username = "testuser";

            // Act
            var query = new GetProfileByUsernameQuery(options)
            {
                Username = username
            };

            // Assert
            Assert.Equal(username, query.Username);
        }
    }
}
