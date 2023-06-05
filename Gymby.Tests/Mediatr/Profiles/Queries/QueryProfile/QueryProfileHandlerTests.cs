using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.QueryProfile
{
    public class QueryProfileHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IMediator _mediator;

        public QueryProfileHandlerTests()
        {
            ProfileCommandTestFixture fixture = new();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task QueryProfileHandler_SearchAmongAllUsers_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryProfile = new QueryProfileHandler(Context, Mapper, _mediator);
            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            var result = await handlerQueryProfile.Handle(new QueryProfileQuery()
            {
                Query = "user",
                Options = appConfigOptionsProfile,
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user => Assert.True(user.Username?.Contains("user")));
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async Task QueryProfileHandler_SearchAmongCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryProfile = new QueryProfileHandler(Context, Mapper, _mediator);
            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            var result = await handlerQueryProfile.Handle(new QueryProfileQuery()
            {
                Query = "user",
                Options = appConfigOptionsProfile,
                Type = "trainers",
                UserId = ProfileContextFactory.UserAId.ToString(),
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
            var handlerQueryProfile = new QueryProfileHandler(Context, Mapper, _mediator);
            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            var result = await handlerQueryProfile.Handle(new QueryProfileQuery()
            {
                Query = "nononononono",
                Options = appConfigOptionsProfile,
                UserId = ProfileContextFactory.UserAId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task QueryProfileHandler_SearchWhenOneUserHasThisUsername_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryProfile = new QueryProfileHandler(Context, Mapper, _mediator);
            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            var result = await handlerQueryProfile.Handle(new QueryProfileQuery()
            {
                Query = "bill",
                Options = appConfigOptionsProfile,
                UserId = ProfileContextFactory.UserAId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user => Assert.True(user.Username?.Contains("bill")));
            Assert.Single(result);
        }
    }
}
