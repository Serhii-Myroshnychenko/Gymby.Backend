using AutoMapper;

namespace Gymby.UnitTests.Mediatr.Profiles.Commands.UpdateProfile
{
    public class UpdateProfileHandlerTests 
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public UpdateProfileHandlerTests()
        {
            ProfileCommandTestFixture fixture = new ProfileCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task UpdateProfileHandler_ShouldBeSuccess()
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

            var appConfigOptions = Options.Create(new AppConfig());

            var diary = new Diary
            {
                Id = Guid.NewGuid().ToString(),
                Name = "My Diary",
                CreationDate = DateTime.Now
            };

            var diaryAccess = new Gymby.Domain.Entities.DiaryAccess
            {
                Id = Guid.NewGuid().ToString(),
                UserId = ProfileContextFactory.UserBId.ToString(),
                DiaryId = diary.Id,
                Type = AccessType.Owner,
                Diary = diary
            };

            Context.Diaries.Add(diary);
            Context.DiaryAccess.Add(diaryAccess);
            await Context.SaveChangesAsync();

            // Act
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
        public async Task UpdateProfileHandler_ShouldBeFailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateProfileHandler(Context, Mapper, FileService);
            var updateUserId = "userB1";

            var appConfigOptions = Options.Create(new AppConfig());

            // Act
            // Assert
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
