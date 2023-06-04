using Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.QueryProfile
{
    public class QueryProfileQueryTests
    {
        [Fact]
        public void QueryProfileQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var query = "testQuery";
            var type = "testCoach";

            // Act
            var command = new QueryProfileQuery
            {
                Query = query,
                Type = type
            };

            // Assert
            Assert.Equal(query, command.Query);
            Assert.Equal(type, command.Type);
        }
    }
}
