using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramAccesses.AccessProgramToUserByUsername;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.Application.Mediatr.Programs.Queries.GetAllProgramsInDiary;
using Gymby.UnitTests.Common.Programs;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.GetAllProgramsInDiary
{
    public class GetAllProgramsInDiaryHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetAllProgramsInDiaryHandlerTests()
        {
            ProgramCommandTestFixture fixture = new ProgramCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetAllProgramsInDiaryHandler_WhenAvailableFreePersonalFromCoachPrograms_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessProgram = new AccessProgramToUserByUsernameHandler(Context);
            var handlerGetAllProgramsInDiary = new GetAllProgramsInDiaryHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var user1 = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user1 != null)
            {
                user1.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserAId.ToString());
            if (user != null)
            {
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var createProgramForShareToUser = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "createProgramForShareToUser",
                Description = "SHARE FROM COACH",
                Level = "Beginner",
                Type = "WeightGain"
            }, CancellationToken.None);

            var createProgramForPublic = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "createProgramForPublic",
                Description = "FREE",
                Level = "Beginner",
                Type = "WeightGain"
            }, CancellationToken.None);

            var createProgramForShareToUserId = createProgramForShareToUser.Id;
            var createProgramForPublicId = createProgramForPublic.Id;

            await handlerAccessProgram.Handle(new AccessProgramToUserByUsernameQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                ProgramId = createProgramForShareToUserId,
                Username = ProfileContextFactory.FriendForPending
            }, CancellationToken.None);

            var program = await Context.Programs.FirstOrDefaultAsync(u => u.Id == createProgramForPublicId);
            if (program != null)
            {
                program.IsPublic = true;
                await Context.SaveChangesAsync();
            }

            await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Name = "Program PERSONAL",
                Description = "PERSONAL",
                Level = "Advanced",
                Type = "WeightGain"
            }, CancellationToken.None);

            // Act
            var resultGetAllProgramsInDiary = await handlerGetAllProgramsInDiary.Handle(new GetAllProgramsInDiaryQuery()
            {
                UserId = ProfileContextFactory.UserAId.ToString()
            }, CancellationToken.None);

            // Assert
            resultGetAllProgramsInDiary.Count.Should().Be(3);
        }

        [Fact]
        public async Task GetAllProgramsInDiaryHandler_WhenAvailableFreeAndPersonalPrograms_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessProgram = new AccessProgramToUserByUsernameHandler(Context);
            var handlerGetAllProgramsInDiary = new GetAllProgramsInDiaryHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var user1 = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user1 != null)
            {
                user1.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserAId.ToString());
            if (user != null)
            {
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var createProgramForPublic = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "createProgramForPublic",
                Description = "FREE",
                Level = "Beginner",
                Type = "WeightGain"
            }, CancellationToken.None);

            var createProgramForPublicId = createProgramForPublic.Id;

            var program = await Context.Programs.FirstOrDefaultAsync(u => u.Id == createProgramForPublicId);
            if (program != null)
            {
                program.IsPublic = true;
                await Context.SaveChangesAsync();
            }

            await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Name = "Program PERSONAL",
                Description = "PERSONAL",
                Level = "Advanced",
                Type = "WeightGain"
            }, CancellationToken.None);

            // Act
            var resultGetAllProgramsInDiary = await handlerGetAllProgramsInDiary.Handle(new GetAllProgramsInDiaryQuery()
            {
                UserId = ProfileContextFactory.UserAId.ToString()
            }, CancellationToken.None);

            // Assert
            resultGetAllProgramsInDiary.Count.Should().Be(2);
        }

        [Fact]
        public async Task GetAllProgramsInDiaryHandler_WhenAvailableOnlyFreeProgram_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessProgram = new AccessProgramToUserByUsernameHandler(Context);
            var handlerGetAllProgramsInDiary = new GetAllProgramsInDiaryHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var user1 = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user1 != null)
            {
                user1.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserAId.ToString());
            if (user != null)
            {
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var createProgramForPublic = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "createProgramForPublic",
                Description = "FREE",
                Level = "Beginner",
                Type = "WeightGain"
            }, CancellationToken.None);

            var createProgramForPublicId = createProgramForPublic.Id;

            var program = await Context.Programs.FirstOrDefaultAsync(u => u.Id == createProgramForPublicId);
            if (program != null)
            {
                program.IsPublic = true;
                await Context.SaveChangesAsync();
            }

            // Act
            var resultGetAllProgramsInDiary = await handlerGetAllProgramsInDiary.Handle(new GetAllProgramsInDiaryQuery()
            {
                UserId = ProfileContextFactory.UserAId.ToString()
            }, CancellationToken.None);

            // Assert
            resultGetAllProgramsInDiary.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetAllProgramsInDiaryHandler_WhenAvailableOnlyPersonalProgram_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessProgram = new AccessProgramToUserByUsernameHandler(Context);
            var handlerGetAllProgramsInDiary = new GetAllProgramsInDiaryHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

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

                await handlerProgram.Handle(new CreateProgramCommand()
                {
                    UserId = ProfileContextFactory.UserBId.ToString(),
                    Name = "Program PERSONAL",
                    Description = "PERSONAL",
                    Level = "Advanced",
                    Type = "WeightGain"
                }, CancellationToken.None);

                // Act
                var resultGetAllProgramsInDiary = await handlerGetAllProgramsInDiary.Handle(new GetAllProgramsInDiaryQuery()
                {
                    UserId = ProfileContextFactory.UserBId.ToString()
                }, CancellationToken.None);

                // Assert
                resultGetAllProgramsInDiary.Count.Should().Be(1);
            }
        }

        [Fact]
        public async Task GetAllProgramsInDiaryHandler_WhenAvailableOnlyFromCoachProgram_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessProgram = new AccessProgramToUserByUsernameHandler(Context);
            var handlerGetAllProgramsInDiary = new GetAllProgramsInDiaryHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var user1 = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user1 != null)
            {
                user1.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserAId.ToString());
            if (user != null)
            {
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var createProgramForShareToUser = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "createProgramForShareToUser",
                Description = "SHARE FROM COACH",
                Level = "Beginner",
                Type = "WeightGain"
            }, CancellationToken.None);

            var createProgramForShareToUserId = createProgramForShareToUser.Id;

            await handlerAccessProgram.Handle(new AccessProgramToUserByUsernameQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                ProgramId = createProgramForShareToUserId,
                Username = ProfileContextFactory.FriendForPending
            }, CancellationToken.None);

            // Act
            var resultGetAllProgramsInDiary = await handlerGetAllProgramsInDiary.Handle(new GetAllProgramsInDiaryQuery()
            {
                UserId = ProfileContextFactory.UserAId.ToString()
            }, CancellationToken.None);

            // Assert
            resultGetAllProgramsInDiary.Count.Should().Be(1);
        }
    }
}
