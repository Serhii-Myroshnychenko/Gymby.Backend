using Gymby.Application.Mediatr.Friends.Queries.GetPendingFriendsList;

namespace Gymby.UnitTests.Mediatr.Friends.Queries.GetPendingFriendsList
{
    public class GetPendingFriendsListQueryTests
    {
        [Fact]
        public void GetMyFriendsListQuery_ShouldBeInitialized()
        {
            // Arrange
            var appConfigOptionsFriend = Options.Create(new AppConfig());
            var query = new GetPendingFriendsListQuery(appConfigOptionsFriend);
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
