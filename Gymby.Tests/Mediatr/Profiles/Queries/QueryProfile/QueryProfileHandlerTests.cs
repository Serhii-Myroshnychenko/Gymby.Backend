using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.QueryProfile
{
    public class QueryProfileHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public QueryProfileHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task QueryProfileHandler_SearchAmongAllUsers_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryProfile = new QueryProfileHandler(Context, Mapper);

            // Act
            var result = await handlerQueryProfile.Handle(new QueryProfileQuery()
            {
                Query = "user"
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user => Assert.True(user.Username?.Contains("user")));
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task QueryProfileHandler_SearchAmongCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryProfile = new QueryProfileHandler(Context, Mapper);

            // Act
            var result = await handlerQueryProfile.Handle(new QueryProfileQuery()
            {
                Query = "user",
                Type = "trainers"
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user => Assert.True(user.IsCoach && user.Username.Contains("user")));
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task QueryProfileHandler_SearchNonexestentUsername_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryProfile = new QueryProfileHandler(Context, Mapper);

            // Act
            var result = await handlerQueryProfile.Handle(new QueryProfileQuery()
            {
                Query = "nononononono"
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task QueryProfileHandler_SearchWhenOneUserHasThisUsername_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryProfile = new QueryProfileHandler(Context, Mapper);

            // Act
            var result = await handlerQueryProfile.Handle(new QueryProfileQuery()
            {
                Query = "bill"
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user => Assert.True(user.Username?.Contains("bill")));
            Assert.Single(result);
        }
    }
}
