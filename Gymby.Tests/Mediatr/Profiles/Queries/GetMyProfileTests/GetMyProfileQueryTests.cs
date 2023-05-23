using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.GetMyProfileTests
{
    public class GetMyProfileQueryTests
    {
        [Fact]
        public void GetMyProfileQuery_ShouldSetOptions()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;

            // Act
            var query = new GetMyProfileQuery(options);

            // Assert
            Assert.Same(options, query.Options);
        }

        [Fact]
        public void GetMyProfileQuery_ShouldSetProperties()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var userId = "userId";
            var email = "test@example.com";

            // Act
            var query = new GetMyProfileQuery(options)
            {
                UserId = userId,
                Email = email
            };

            // Assert
            Assert.Equal(userId, query.UserId);
            Assert.Equal(email, query.Email);
        }
    }
}
