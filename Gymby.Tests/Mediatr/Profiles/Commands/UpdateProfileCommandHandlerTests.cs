using AutoMapper;

namespace Gymby.UnitTests.Mediatr.Profiles.Commands
{
    public class UpdateProfileCommandHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public UpdateProfileCommandHandlerTests()
        {
            CommandTestFixture fixture = new CommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task UpdateProfileHandler_Success()
        {
            // Arrange
            var handler = new UpdateProfileHandler(Context, Mapper, FileService);
            var updateFirstName = "UserB_Edit";
            var updateLastName = "Bill_Edit";
            var updateUsername = "user-bill-edit";
            var updateEmail = "userb-edit@gmail.com";
            var updateProfileId = "userB1";
            var updatedDescroption = "UserB Bill test EDIT";
            var updatedInstagramUrl = "instagram-user-b-edit";
            var updatedFacebookUrl = "facebook-user-b-edit";
            var updatedTelegramUsername = "telegram-user-b-edit";

            // Act
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);

            await handler.Handle(new UpdateProfileCommand(appConfigOptions)
            {
                ProfileId = updateProfileId,
                UserId = ProfileContextFactory.UserBId.ToString(),
                FirstName = updateFirstName,
                LastName = updateLastName,
                Username = updateUsername,
                Email = updateEmail,
                Description = updatedDescroption,
                InstagramUrl = updatedInstagramUrl,
                FacebookUrl = updatedFacebookUrl,
                TelegramUsername = updatedTelegramUsername,
            }, CancellationToken.None);

            // Assert
            var a = await Context.Profiles.ToListAsync();
            Assert.NotNull(await Context.Profiles.SingleOrDefaultAsync(profile =>
            profile.Id == updateProfileId &&
            profile.Username == updateUsername && profile.Email == updateEmail));
        }

        [Fact]
        public async Task UpdateProfileHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateProfileHandler(Context, Mapper, FileService);
            var updateUserId = "userB1";

            // Act
            // Assert
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);

            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                    new UpdateProfileCommand(appConfigOptions)
                    {
                        ProfileId = Guid.NewGuid().ToString(),
                        UserId = updateUserId,
                    },
                    CancellationToken.None));
        }
    }
}
