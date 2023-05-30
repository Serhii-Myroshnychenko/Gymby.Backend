using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.GetMyProfileTests
{
    public class GetMyProfileQueryTests
    {
        [Fact]
        public void GetMyProfileQuery_ShouldBeSetOptions()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;

            // Act
            var result = new GetMyProfileQuery(options);

            // Assert
            Assert.Same(options, result.Options);
        }

        [Fact]
        public void GetMyProfileQuery_ShouldBeSetProperties()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var userId = "userId";
            var email = "test@example.com";

            // Act
            var result = new GetMyProfileQuery(options)
            {
                UserId = userId,
                Email = email
            };

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(email, result.Email);
        }
    }
}
