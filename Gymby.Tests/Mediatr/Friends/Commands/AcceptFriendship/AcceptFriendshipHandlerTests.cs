using AutoMapper;
using Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;
using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.AcceptFriendship
{
    public class AcceptFriendshipHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public AcceptFriendshipHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task AcceptFriendshipHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerForAccept = new AcceptFriendshipHandler(Context, Mapper, FileService);
            var handlerForInvite = new InviteFriendHandler(Context);

            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            // Act
            var result = await handlerForAccept.Handle(new AcceptFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject,
            }, CancellationToken.None);

            var savedFriendship = await Context.Friends.FirstOrDefaultAsync(f => f.SenderId == ProfileContextFactory.UserAId.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(savedFriendship);
            Assert.Equal(ProfileContextFactory.UserAId.ToString(), savedFriendship.SenderId);
            Assert.Equal(ProfileContextFactory.UserBId.ToString(), savedFriendship.ReceiverId);
            Assert.Equal(Status.Confirmed, savedFriendship.Status);
        }

        [Fact]
        public async Task AcceptFriendshipHandler_ShouldBeNotFoundEntityExceptionForProfile()
        {
            // Arrange
            var handler = new AcceptFriendshipHandler(Context, Mapper, FileService);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(new AcceptFriendshipCommand()
            {
                UserId = Guid.NewGuid().ToString(),
                Username = Guid.NewGuid().ToString()
            }, CancellationToken.None));
        }

        [Fact]
        public async Task AcceptFriendshipHandler_ShouldBeNotFoundEntityExceptionForFriendship()
        {
            // Arrange
            var handler = new AcceptFriendshipHandler(Context, Mapper, FileService);
            var handlerForInvite = new InviteFriendHandler(Context);

            await handlerForInvite.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            // Act
            await handler.Handle(new AcceptFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(new AcceptFriendshipCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForAcceptOrReject
            }, CancellationToken.None));
        }
    }
}
