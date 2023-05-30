using AutoMapper;
using Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;
using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;
using Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;

namespace Gymby.UnitTests.Mediatr.Friends.Queries.GetMyFriendsList
{
    public class GetMyFriendsListHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetMyFriendsListHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetMyFriendsListHandler_WhenUserHasTwoFriends_ShouldBeSuccess()
        {
            // Arrange
            var handler = new GetMyFriendsListHandler(Context, Mapper, FileService);
            var handlerForAccept = new AcceptFriendshipHandler(Context);
            var handlerForInvite = new InviteFriendHandler(Context);
            var appConfigOptionsFriend= Options.Create(new AppConfig());

            // Act
            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            await handlerForAccept.Handle(new AcceptFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None);

            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite2
            }, CancellationToken.None);

            await handlerForAccept.Handle(new AcceptFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserDId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None);

            var result = await handler.Handle(new GetMyFriendsListQuery(appConfigOptionsFriend)
                {
                    UserId = ProfileContextFactory.UserAId.ToString(),
                }, CancellationToken.None);

            // Assert
            result.Count.ShouldBe(2);
            Assert.Equal("user-bill", result[0].Username);
            Assert.Equal("user-den", result[1].Username);
        }

        [Fact]
        public async Task GetMyFriendsListHandler_WhenUserHasZeroFriends_ShouldBeSuccess()
        {
            // Arrange
            var handler = new GetMyFriendsListHandler(Context, Mapper, FileService);
            var appConfigOptionsFriend = Options.Create(new AppConfig());

            // Act
            var result = await handler.Handle(new GetMyFriendsListQuery(appConfigOptionsFriend)
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
            }, CancellationToken.None);

            // Assert
            result.Count.ShouldBe(0);
            Assert.NotNull(result);
        }
    }
}
