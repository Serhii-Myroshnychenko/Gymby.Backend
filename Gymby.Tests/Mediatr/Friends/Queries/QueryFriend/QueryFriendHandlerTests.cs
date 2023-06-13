using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;
using Gymby.UnitTests.Common.Friends;

namespace Gymby.UnitTests.Mediatr.Friends.Queries.QueryFriend
{
    public class QueryFriendHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;
        private readonly IMediator _mediator;

        public QueryFriendHandlerTests()
        {
            FriendCommandTestFixture fixture = new FriendCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task QueryFriendHandler_SearchOneFriend_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryFriend = new QueryProfileHandler(Context, Mapper, _mediator, FileService);
            var appConfigOptionsFriend = Options.Create(new AppConfig());

            // Act
            var result = await handlerQueryFriend.Handle(new QueryProfileQuery()
            {
                Query = "den",
                Options = appConfigOptionsFriend,
                UserId = FriendContextFactory.FriendAId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user => Assert.True(user.Username?.Contains("den")));
            Assert.Single(result);
        }

        [Fact]
        public async Task QueryFriendHandler_SearchAllFriend_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryFriend = new QueryProfileHandler(Context, Mapper, _mediator, FileService);
            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            var result = await handlerQueryFriend.Handle(new QueryProfileQuery()
            {
                Query = "user",
                Options = appConfigOptionsProfile,
                UserId = FriendContextFactory.FriendAId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user => Assert.True(user.Username?.Contains("user")));
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task QueryFriendHandler_SearchOneCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryFriend = new QueryProfileHandler(Context, Mapper, _mediator, FileService);
            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            var result = await handlerQueryFriend.Handle(new QueryProfileQuery()
            {
                Query = "user-den",
                Type = "trainers",
                Options = appConfigOptionsProfile,
                UserId = FriendContextFactory.FriendAId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user =>
            {
                Assert.True(user.Username?.Contains("user-den"));
                Assert.True(user.IsCoach);
            });
            Assert.Single(result);
        }

        [Fact]
        public async Task QueryFriendHandler_SearchAllCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerQueryFriend = new QueryProfileHandler(Context, Mapper, _mediator, FileService);
            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            var result = await handlerQueryFriend.Handle(new QueryProfileQuery()
            {
                Query = "user",
                Type = "trainers",
                Options = appConfigOptionsProfile,
                UserId = FriendContextFactory.FriendAId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, user =>
            {
                Assert.True(user.Username?.Contains("user"));
                Assert.True(user.IsCoach);
            });
            Assert.Equal(2, result.Count);
        }
    }
}
