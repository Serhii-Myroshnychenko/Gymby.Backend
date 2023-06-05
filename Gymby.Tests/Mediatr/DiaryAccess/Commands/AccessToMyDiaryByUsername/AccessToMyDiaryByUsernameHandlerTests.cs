using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.DiaryAccesses.Commands.AccessToMyDiaryByUsername;
using Gymby.UnitTests.Common.Exercise;

namespace Gymby.UnitTests.Mediatr.DiaryAccess.Commands.AccessToMyDiaryByUsername
{
    public class AccessToMyDiaryByUsernameHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public AccessToMyDiaryByUsernameHandlerTests()
        {
            ProgramExerciseCommandTestFixture fixture = new ProgramExerciseCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task AccessToMyDiaryByUsernameHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessToMyDiaryByUsername = new AccessToMyDiaryByUsernameHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            var profileId = Guid.NewGuid().ToString();

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

            var profile = new Gymby.Domain.Entities.Profile
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "UserC",
                LastName = "Chandler",
                Email = "user-c@gmail.com",
                Username = "user-chandler",
                IsCoach = false,
                Description = "UserC Chandler test",
                PhotoAvatarPath = "https://user3.azurewebsites.net",
                InstagramUrl = "instagram-user-c",
                FacebookUrl = "facebook-user-c",
                TelegramUsername = "telegram-user-c",
                UserId = profileId.ToString()
            };

            Context.Diaries.Add(diary);
            Context.DiaryAccess.Add(diaryAccess);
            Context.Profiles.Add(profile);
            await Context.SaveChangesAsync();

            var diaryId = diary.Id;

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());

            if (user != null)
            {
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var result = await handlerAccessToMyDiaryByUsername.Handle(new AccessToMyDiaryByUsernameCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = "user-chandler"
            }, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task AccessToMyDiaryByUsernameHandler_ShouldBeFail()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessToMyDiaryByUsername = new AccessToMyDiaryByUsernameHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            var friendException = Guid.NewGuid().ToString();

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

            var diaryId = diary.Id;

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());

            if (user != null)
            {
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerAccessToMyDiaryByUsername.Handle(new AccessToMyDiaryByUsernameCommand()
                {
                    UserId = ProfileContextFactory.UserBId.ToString(),
                    Username = friendException
                }, CancellationToken.None);
            });

            Assert.Equal($"Entity \"{friendException}\" ({nameof(Domain.Entities.Profile)}) not found", exception.Message);
        }
    }
}
