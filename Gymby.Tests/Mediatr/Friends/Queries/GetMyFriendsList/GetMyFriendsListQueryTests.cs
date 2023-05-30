using Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;
using Microsoft.AspNetCore.Http;

namespace Gymby.UnitTests.Mediatr.Friends.Queries.GetMyFriendsList
{
    public class GetMyFriendsListQueryTests
    {
        [Fact]
        public void GetMyFriendsListQuery_ShouldBeInitialized()
        {
            // Arrange
            var appConfigOptionsFriend = Options.Create(new AppConfig());
            var query = new GetMyFriendsListQuery(appConfigOptionsFriend);
            var userId = "testUser";

            // Act
            query.UserId = userId;

            // Assert
            Assert.NotNull(query);
            Assert.NotNull(query.UserId);
            Assert.Equal(appConfigOptionsFriend, query.Options);
            Assert.Equal(userId, query.UserId);
        }
    }
}
