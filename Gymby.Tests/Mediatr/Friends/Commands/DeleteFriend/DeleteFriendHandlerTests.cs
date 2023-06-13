using AutoMapper;
using Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;
using Gymby.Application.Mediatr.Friends.Commands.DeleteFriend;
using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.DeleteFriend
{
    public class DeleteFriendHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public DeleteFriendHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task DeleteFriendHandler_DeleteSenderFromReceiver_ShouldBeSuccess()
        {
            // Arrange
            var handlerForAccept = new AcceptFriendshipHandler(Context, Mapper, FileService);
            var handlerForInvite = new InviteFriendHandler(Context);
            var handlerForDelete = new DeleteFriendHandler(Context, Mapper, FileService);

            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            await handlerForAccept.Handle(new AcceptFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject,
            }, CancellationToken.None);

            // Act
            var delete = await handlerForDelete.Handle(new DeleteFriendCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject,
            }, CancellationToken.None);

            var savedFriendship = await Context.Friends.FirstOrDefaultAsync(f => f.ReceiverId == ProfileContextFactory.UserBId.ToString());

            // Assert
            Assert.Null(savedFriendship);
        }

        [Fact]
        public async Task DeleteFriendHandler_DeleteReceiverFromSender_ShouldBeSuccess()
        {
            // Arrange
            var handlerForAccept = new AcceptFriendshipHandler(Context, Mapper, FileService);
            var handlerForInvite = new InviteFriendHandler(Context);
            var handlerForDelete = new DeleteFriendHandler(Context, Mapper, FileService);

            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            await handlerForAccept.Handle(new AcceptFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject,
            }, CancellationToken.None);

            // Act
            var delete = await handlerForDelete.Handle(new DeleteFriendCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject,
            }, CancellationToken.None);

            var savedFriendship = await Context.Friends.FirstOrDefaultAsync(f => f.SenderId == ProfileContextFactory.UserAId.ToString());

            // Assert
            Assert.Null(savedFriendship);
        }

        [Fact]
        public async Task DeleteFriendHandler_ShouldThrowNotFoundEntityException()
        {
            // Arrange
            var handlerForDelete = new DeleteFriendHandler(Context, Mapper, FileService);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerForDelete.Handle(new DeleteFriendCommand()
                {
                    UserId = ProfileContextFactory.UserBId.ToString(),
                    Username = "noneTest",
                }, CancellationToken.None);
            });
        }
    }
}
