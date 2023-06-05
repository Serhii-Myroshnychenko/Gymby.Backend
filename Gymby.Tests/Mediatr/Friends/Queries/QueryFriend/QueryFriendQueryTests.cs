using Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;

namespace Gymby.UnitTests.Mediatr.Friends.Queries.QueryFriend
{
    public class QueryFriendQueryTests
    {
        [Fact]
        public void QueryFriendQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var query = "testQuery";
            var type = "testCoach";
            var userId = "testUserId";

            // Act
            var command = new QueryProfileQuery
            {
                Query = query,
                Type = type,
                UserId = userId
            };

            // Assert
            Assert.Equal(query, command.Query);
            Assert.Equal(type, command.Type);
            Assert.Equal(userId, command.UserId);
        }
    }
}
