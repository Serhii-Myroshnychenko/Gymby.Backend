using AutoMapper;
using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;
using Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;
using Gymby.Application.Mediatr.Friends.Queries.GetPendingFriendsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Friends.Queries.GetPendingFriendsList
{

    public class GetPendingFriendsListHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetPendingFriendsListHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetPendingFriendsListHandlerTests_WhenUserHasTwoPendingFriends_ShouldBeSuccess()
        {
            // Arrange
            var handler = new GetPendingFriendsListHandler(Context, Mapper, FileService);
            var handlerForInvite = new InviteFriendHandler(Context);
            var appConfigOptionsFriend = Options.Create(new AppConfig());

            // Act
            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None);

            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserDId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None);

            var result = await handler.Handle(new GetPendingFriendsListQuery(appConfigOptionsFriend)
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
            }, CancellationToken.None);

            // Assert
            result.Count.ShouldBe(2);
            Assert.Equal("user-bill", result[0].Username);
            Assert.Equal("user-den", result[1].Username);
        }

        [Fact]
        public async Task GetPendingFriendsListHandlerTests_WhenUserHasZeroPendingFriends_ShouldBeSuccess()
        {
            // Arrange
            var handler = new GetPendingFriendsListHandler(Context, Mapper, FileService);
            var appConfigOptionsFriend = Options.Create(new AppConfig());

            // Act
            var result = await handler.Handle(new GetPendingFriendsListQuery(appConfigOptionsFriend)
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
            }, CancellationToken.None);

            // Assert
            result.Count.ShouldBe(0);
            Assert.NotNull(result);
        }
    }

}
