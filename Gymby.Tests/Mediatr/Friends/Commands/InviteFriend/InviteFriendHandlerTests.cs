using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;

namespace Gymby.UnitTests.Mediatr.Friends.Commands.InviteFriend
{
    public class InviteFriendHandlerTests
    {
        private readonly ApplicationDbContext Context;

        public InviteFriendHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
        }

        [Fact]
        public async Task InviteFriendHandler_ShouldBeSuccess()
        {
            // Arrange
            var handler = new InviteFriendHandler(Context);

            // Act
            var result = await handler.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            var savedFriendship = await Context.Friends.FirstOrDefaultAsync(f => f.Id == result);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotNull(savedFriendship);
            Assert.Equal(ProfileContextFactory.UserAId.ToString(), savedFriendship.SenderId);
            Assert.Equal(ProfileContextFactory.UserBId.ToString(), savedFriendship.ReceiverId);
            Assert.Equal(Status.Pending, savedFriendship.Status);
        }

        [Fact]
        public async Task InviteFriendHandler_ShouldBeNotFoundEntityException()
        {
            // Arrange
            var handler = new InviteFriendHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            await handler.Handle(new InviteFriendCommand()
            {
                UserId = Guid.NewGuid().ToString(),
                Username = Guid.NewGuid().ToString()
            }, CancellationToken.None));
        }

        [Fact]
        public async Task InviteFriendHandler_ShouldBeInviteFriendException()
        {
            // Arrange
            var handler = new InviteFriendHandler(Context);

            // Act
            await handler.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<InviteFriendException>(async () =>
            await handler.Handle(new InviteFriendCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Username = ProfileContextFactory.FriendUsernameForInvite
            }, CancellationToken.None));
        }
    }
}
