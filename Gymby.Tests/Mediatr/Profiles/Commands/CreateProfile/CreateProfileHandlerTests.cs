using Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Commands.CreateProfile
{
    public class CreateProfileHandlerTests
    {
        private readonly ApplicationDbContext Context;

        public CreateProfileHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
        }

        [Fact]
        public async Task CreateProfileHandler_WhenNewUser_ShouldBeSuccess()
        {
            // Arrange
            var handler = new CreateProfileHandler(Context);
            var email = "userb-edit@gmail.com";

            // Act
            var profileId = await handler.Handle(new CreateProfileCommand()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = email,
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Profiles.SingleOrDefaultAsync(profile =>
                profile.Email == email &&
                profile.Username.Length == 11));
        }

        [Fact]
        public async Task CreateProfileHandler_WhenUserExists_ShouldBeReturnUnitValue()
        {
            // Arrange
            var handler = new CreateProfileHandler(Context);

            // Act
            var result = await handler.Handle(new CreateProfileCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
        }
    }
}
