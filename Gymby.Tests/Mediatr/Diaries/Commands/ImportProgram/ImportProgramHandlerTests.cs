using AutoMapper;
using Gymby.UnitTests.Common.Diaries;
using Gymby.Application.Mediatr.Approaches.Commands.UpdateProgramApproach;
using Gymby.Application.Mediatr.Diaries.Command.CreateDiary;
using Gymby.Application.Mediatr.Diaries.Command.ImportProgramDay;
using Gymby.Application.Mediatr.DiaryAccesses.Commands.AccessToMyDiaryByUsername;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Exercise;
using Gymby.Application.Mediatr.Diaries.Command.ImportProgram;
using Microsoft.EntityFrameworkCore;

namespace Gymby.UnitTests.Mediatr.Diaries.Commands.ImportProgram
{
    public class ImportProgramHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;
        private readonly IMediator _mediator;

        public ImportProgramHandlerTests()
        {
            DiaryCommandTestSixture fixture = new DiaryCommandTestSixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task ImportProgramHandler_InMyDiary_WhenProgramHasTwoDaysAndTwoDayOfWeek_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerCreateDiary = new CreateDiaryHandler(Context, Mapper, FileService);
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerImportProgramDay = new ImportProgramDayHandler(Context, Mapper);
            var handlerImportProgram = new ImportProgramHandler(Context, Mapper, _mediator);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var ExercisePrototypeId_A = Guid.NewGuid().ToString();

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);
            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user != null)
            {
                user.Username = "user-bill";
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var resultCreateDiary = await handlerCreateDiary.Handle(new CreateDiaryCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var diary = Context.Diaries.FirstOrDefault(d => d.Name == "user-bill diary");

            var resultProgram = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "ProgramName1",
                Description = "Description1",
                Level = "Advanced",
                Type = "WeightGain"
            }, CancellationToken.None);

            var programId = resultProgram.Id;

            var resultProgramDay = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName1",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var resultProgramDay2 = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName2",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var programDayId = resultProgramDay.Id;
            var programDayId2 = resultProgramDay2.Id;

            var exercisePrototypeAdd = new Gymby.Domain.Entities.ExercisePrototype
            {
                Id = ExercisePrototypeId_A,
                Name = "Chest",
                Description = "Chect desc",
                Category = Category.Chest
            };
            Context.ExercisePrototypes.Add(exercisePrototypeAdd);
            await Context.SaveChangesAsync();

            var resultExercisePrototype = await handlerExercisePrototype.Handle(new GetAllExercisePrototypesQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var exercisePrototype = resultExercisePrototype[0].Id;
            DateTime dateValue = DateTime.Parse("2023-06-15T14:29:43.385Z");

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName1",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var resultProgramExercise2 = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId2,
                Name = "ExerciseName2",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var days = new List<string> { "Monday", "Sunday" };
            var resultImport = await handlerImportProgram.Handle(new ImportProgramCommand()
            {
                ProgramId = programId,
                DiaryId = null,
                StartDate = dateValue,
                DaysOfWeek = days,
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var resultDiaryDays = diary?.DiaryDays.Where(d => d.DiaryId == diary.Id).ToArray();

            // Assert
            Assert.NotNull(diary);
            Assert.Equal(Unit.Value, resultImport);
            Assert.Equal(2, Context.DiaryDays.Count());
            Assert.Equal(new DateTime(2023, 6, 18), resultDiaryDays?[0]?.Date);
            Assert.Equal(new DateTime(2023, 6, 19), resultDiaryDays?[1]?.Date);
        }

        [Fact]
        public async Task ImportProgramHandler_InMyDiary_WhenProgramHasOneDaysAndTwoDayOfWeek_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerCreateDiary = new CreateDiaryHandler(Context, Mapper, FileService);
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerImportProgramDay = new ImportProgramDayHandler(Context, Mapper);
            var handlerImportProgram = new ImportProgramHandler(Context, Mapper, _mediator);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var ExercisePrototypeId_A = Guid.NewGuid().ToString();

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);
            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user != null)
            {
                user.Username = "user-bill";
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var resultCreateDiary = await handlerCreateDiary.Handle(new CreateDiaryCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var diary = Context.Diaries.FirstOrDefault(d => d.Name == "user-bill diary");

            var resultProgram = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "ProgramName1",
                Description = "Description1",
                Level = "Advanced",
                Type = "WeightGain"
            }, CancellationToken.None);

            var programId = resultProgram.Id;

            var resultProgramDay = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName1",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var programDayId = resultProgramDay.Id;

            var exercisePrototypeAdd = new Gymby.Domain.Entities.ExercisePrototype
            {
                Id = ExercisePrototypeId_A,
                Name = "Chest",
                Description = "Chect desc",
                Category = Category.Chest
            };
            Context.ExercisePrototypes.Add(exercisePrototypeAdd);
            await Context.SaveChangesAsync();

            var resultExercisePrototype = await handlerExercisePrototype.Handle(new GetAllExercisePrototypesQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var exercisePrototype = resultExercisePrototype[0].Id;
            DateTime dateValue = DateTime.Parse("2023-06-15T14:29:43.385Z");

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName1",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var days = new List<string> { "Sunday" };
            var resultImport = await handlerImportProgram.Handle(new ImportProgramCommand()
            {
                ProgramId = programId,
                DiaryId = null,
                StartDate = dateValue,
                DaysOfWeek = days,
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var resultDiaryDays = diary?.DiaryDays.Where(d => d.DiaryId == diary.Id).ToArray();

            // Assert
            Assert.NotNull(diary);
            Assert.Equal(Unit.Value, resultImport);
            Assert.Equal(1, Context.DiaryDays.Count());
            Assert.Equal(new DateTime(2023, 6, 18), resultDiaryDays?[0]?.Date);
        }

        [Fact]
        public async Task ImportProgramHandler_InMyDiary_WhenProgramHasTwoDaysAndOneDayOfWeek_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerCreateDiary = new CreateDiaryHandler(Context, Mapper, FileService);
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerImportProgramDay = new ImportProgramDayHandler(Context, Mapper);
            var handlerImportProgram = new ImportProgramHandler(Context, Mapper, _mediator);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var ExercisePrototypeId_A = Guid.NewGuid().ToString();

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);
            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user != null)
            {
                user.Username = "user-bill";
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var resultCreateDiary = await handlerCreateDiary.Handle(new CreateDiaryCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var diary = Context.Diaries.FirstOrDefault(d => d.Name == "user-bill diary");

            var resultProgram = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "ProgramName1",
                Description = "Description1",
                Level = "Advanced",
                Type = "WeightGain"
            }, CancellationToken.None);

            var programId = resultProgram.Id;

            var resultProgramDay = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName1",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var resultProgramDay2 = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName2",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var programDayId = resultProgramDay.Id;
            var programDayId2 = resultProgramDay2.Id;

            var exercisePrototypeAdd = new Gymby.Domain.Entities.ExercisePrototype
            {
                Id = ExercisePrototypeId_A,
                Name = "Chest",
                Description = "Chect desc",
                Category = Category.Chest
            };
            Context.ExercisePrototypes.Add(exercisePrototypeAdd);
            await Context.SaveChangesAsync();

            var resultExercisePrototype = await handlerExercisePrototype.Handle(new GetAllExercisePrototypesQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var exercisePrototype = resultExercisePrototype[0].Id;
            DateTime dateValue = DateTime.Parse("2023-06-15T14:29:43.385Z");

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName1",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var resultProgramExercise2 = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId2,
                Name = "ExerciseName2",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var days = new List<string> { "Friday" };
            var resultImport = await handlerImportProgram.Handle(new ImportProgramCommand()
            {
                ProgramId = programId,
                DiaryId = null,
                StartDate = dateValue,
                DaysOfWeek = days,
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var resultDiaryDays = diary?.DiaryDays.Where(d => d.DiaryId == diary.Id).ToArray();

            // Assert
            Assert.NotNull(diary);
            Assert.Equal(Unit.Value, resultImport);
            Assert.Equal(2, Context.DiaryDays.Count());
            Assert.Equal(new DateTime(2023, 6, 16), resultDiaryDays?[0]?.Date);
            Assert.Equal(new DateTime(2023, 6, 23), resultDiaryDays?[1]?.Date);
        }

        [Fact]
        public async Task ImportProgramHandler_InSharedDiary_WhenProgramHasOneDaysAndTwoDayOfWeek_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerCreateDiary = new CreateDiaryHandler(Context, Mapper, FileService);
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerImportProgramDay = new ImportProgramDayHandler(Context, Mapper);
            var handlerImportProgram = new ImportProgramHandler(Context, Mapper, _mediator);
            var handlerAccessToMyDiaryByUsername = new AccessToMyDiaryByUsernameHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var ExercisePrototypeId_A = Guid.NewGuid().ToString();

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
                UserId = ProfileContextFactory.UserAId.ToString()
            };
            Context.Profiles.Add(profile);
            await Context.SaveChangesAsync();

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);
            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user != null)
            {
                user.Username = "user-bill";
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);
            var userSecond = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserAId.ToString());
            if (userSecond != null)
            {
                userSecond.Username = "user-chandler";
                userSecond.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var resultCreateDiary = await handlerCreateDiary.Handle(new CreateDiaryCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var diary = Context.Diaries.FirstOrDefault(d => d.Name == "user-bill diary");

            var resultProgram = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "ProgramName1",
                Description = "Description1",
                Level = "Advanced",
                Type = "WeightGain"
            }, CancellationToken.None);

            var programId = resultProgram.Id;

            var resultProgramDay = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName1",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var programDayId = resultProgramDay.Id;

            var exercisePrototypeAdd = new Gymby.Domain.Entities.ExercisePrototype
            {
                Id = ExercisePrototypeId_A,
                Name = "Chest",
                Description = "Chect desc",
                Category = Category.Chest
            };
            Context.ExercisePrototypes.Add(exercisePrototypeAdd);
            await Context.SaveChangesAsync();

            var resultExercisePrototype = await handlerExercisePrototype.Handle(new GetAllExercisePrototypesQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var exercisePrototype = resultExercisePrototype[0].Id;
            DateTime dateValue = DateTime.Parse("2023-06-15T14:29:43.385Z");

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName1",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            await handlerAccessToMyDiaryByUsername.Handle(new AccessToMyDiaryByUsernameCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = "user-chandler"
            }, CancellationToken.None);

            var days = new List<string> { "Friday", "Monday" };
            var resultImport = await handlerImportProgram.Handle(new ImportProgramCommand()
            {
                ProgramId = programId,
                DiaryId = diary?.Id,
                StartDate = dateValue,
                DaysOfWeek = days,
                UserId = ProfileContextFactory.UserAId.ToString(),
            }, CancellationToken.None);

            var resultDiaryDays = diary?.DiaryDays.Where(d => d.DiaryId == diary.Id).ToArray();

            // Assert
            Assert.NotNull(diary);
            Assert.Equal(Unit.Value, resultImport);
            Assert.Equal(AccessType.Shared, Context.DiaryAccess.FirstOrDefault(d => d.UserId == ProfileContextFactory.UserAId.ToString())?.Type);
            Assert.Equal(1, Context.DiaryDays.Count());
            Assert.Equal(new DateTime(2023, 6, 16), resultDiaryDays?[0]?.Date);
        }

        [Fact]
        public async Task ImportProgramHandler_WhenNonexistentProgram_ShouldBeFail()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerCreateDiary = new CreateDiaryHandler(Context, Mapper, FileService);
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerImportProgramDay = new ImportProgramDayHandler(Context, Mapper);
            var handlerImportProgram = new ImportProgramHandler(Context, Mapper, _mediator);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var programId = Guid.NewGuid().ToString();

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);
            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            if (user != null)
            {
                user.Username = "user-bill";
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

            var resultCreateDiary = await handlerCreateDiary.Handle(new CreateDiaryCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var diary = Context.Diaries.FirstOrDefault(d => d.Name == "user-bill diary");

            DateTime dateValue = DateTime.Parse("2023-06-15T14:29:43.385Z");

            var days = new List<string> { "Friday" };

            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerImportProgram.Handle(new ImportProgramCommand()
                {
                    ProgramId = programId,
                    DiaryId = diary?.Id,
                    StartDate = dateValue,
                    DaysOfWeek = days,
                    UserId = ProfileContextFactory.UserBId.ToString(),
                }, CancellationToken.None);

            });

            Assert.Equal($"Entity \"{programId}\" ({nameof(Domain.Entities.Program)}) not found", exception.Message);
        }
    }
}
