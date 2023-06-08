using AutoMapper;
using Gymby.Application.Mediatr.DiaryAccesses.Commands.AccessToMyDiaryByUsername;
using Gymby.Application.Mediatr.DiaryAccesses.Queries.GetAllAvailableDiaries;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.UnitTests.Common.Exercise;

namespace Gymby.UnitTests.Mediatr.DiaryAccess.Queries.GetAllAvailableDiaries
{
    public class GetAllAvailableDiariesHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetAllAvailableDiariesHandlerTests()
        {
            ProgramExerciseCommandTestFixture fixture = new ProgramExerciseCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetAllAvailableDiariesHandler_GetTwoDiaries_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessToMyDiaryByUsername = new AccessToMyDiaryByUsernameHandler(Context, Mapper, FileService);
            var handlerGetAllAvailableDiaries = new GetAllAvailableDiariesHandler(Context);

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

            var diaryChandler = new Diary
            {
                Id = Guid.NewGuid().ToString(),
                Name = "My Diary",
                CreationDate = DateTime.Now
            };

            var diaryAccessChandler = new Gymby.Domain.Entities.DiaryAccess
            {
                Id = Guid.NewGuid().ToString(),
                UserId = profileId.ToString(),
                DiaryId = diaryChandler.Id,
                Type = AccessType.Owner,
                Diary = diaryChandler
            };

            Context.Diaries.Add(diary);
            Context.DiaryAccess.Add(diaryAccess);
            Context.Diaries.Add(diaryChandler);
            Context.DiaryAccess.Add(diaryAccessChandler);
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

            await handlerAccessToMyDiaryByUsername.Handle(new AccessToMyDiaryByUsernameCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = "user-chandler"
            }, CancellationToken.None);

            var result = await handlerGetAllAvailableDiaries.Handle(new GetAllAvailableDiariesQuery()
            {
                UserId = profileId.ToString()
            }, CancellationToken.None);

            // Assert
            result.Count.Should().Be(1);
            result.FirstOrDefault(d => d.DiaryId == diary.Id);
        }

        [Fact]
        public async Task GetAllAvailableDiariesHandler_GetOneDiaries_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerGetAllAvailableDiaries = new GetAllAvailableDiariesHandler(Context);

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
                Type = AccessType.Shared,
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

            var result = await handlerGetAllAvailableDiaries.Handle(new GetAllAvailableDiariesQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            result.Count.Should().Be(1);
            result.FirstOrDefault(d => d.DiaryId == diary.Id);
        }
    }
}
