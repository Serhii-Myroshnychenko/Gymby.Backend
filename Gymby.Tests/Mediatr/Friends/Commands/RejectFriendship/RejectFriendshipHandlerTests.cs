using AutoMapper;
using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;
using Gymby.Application.Mediatr.Friends.Commands.RejectFriendship;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.RejectFriendship
{
    public class RejectFriendshipHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public RejectFriendshipHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task RejectFriendshipHandler_ShouldBeSuccess()
        {
            // Arrange
            var handler = new RejectFriendshipHandler(Context, Mapper, FileService);
            var handlerForInvite = new InviteFriendHandler(Context);

            // Act
            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            var result = await handler.Handle(new RejectFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None);

            var savedFriendship = await Context.Friends.FirstOrDefaultAsync(f => f.SenderId == ProfileContextFactory.UserAId.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(savedFriendship);
            Assert.Equal(ProfileContextFactory.UserAId.ToString(), savedFriendship.SenderId);
            Assert.Equal(ProfileContextFactory.UserBId.ToString(), savedFriendship.ReceiverId);
            Assert.Equal(Status.Rejected, savedFriendship.Status);
        }

        [Fact]
        public async Task RejectFriendshipHandler_ShouldBeNotFoundEntityExceptionForProfile()
        {
            // Arrange
            var handler = new RejectFriendshipHandler(Context, Mapper, FileService);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(new RejectFriendshipCommand()
            {
                UserId = Guid.NewGuid().ToString(),
                Username = Guid.NewGuid().ToString()
            }, CancellationToken.None));
        }

        [Fact]
        public async Task RejectFriendshipHandler_ShouldBeNotFoundEntityExceptionForFriendship()
        {
            // Arrange
            var handler = new RejectFriendshipHandler(Context, Mapper, FileService);
            var handlerForInvite = new InviteFriendHandler(Context);

            // Act
            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            await handler.Handle(new RejectFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(new RejectFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None));
        }
    }
}
