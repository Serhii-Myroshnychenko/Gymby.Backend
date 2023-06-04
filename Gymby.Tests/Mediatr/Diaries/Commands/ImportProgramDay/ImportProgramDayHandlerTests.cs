﻿using AutoMapper;
using Gymby.Application.Mediatr.Diaries.Command.CreateDiary;
using Gymby.Application.Mediatr.Diaries.Command.ImportProgramDay;
using Gymby.Application.Mediatr.DiaryAccesses.Commands.AccessToMyDiaryByUsername;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Exercise;

namespace Gymby.UnitTests.Mediatr.Diaries.Commands.ImportProgramDay
{
    public class ImportProgramDayHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public ImportProgramDayHandlerTests()
        {
            ProgramExerciseCommandTestFixture fixture = new ProgramExerciseCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ImportProgramDayHandler_InMyDiary_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerCreateDiary = new CreateDiaryHandler(Context, Mapper, FileService);
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerImportProgramDay = new ImportProgramDayHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var ExercisePrototypeId_A = Guid.NewGuid().ToString();

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com" 
            }, CancellationToken.None);
            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            user.Username = "user-bill";
            user.IsCoach = true;
            await Context.SaveChangesAsync();

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
                Level = Level.Advanced,
                Type = ProgramType.WeightGain
            }, CancellationToken.None);

            var programId = resultProgram.Id;

            var resultProgramDay = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName",
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
            DateTime dateValue = DateTime.Parse("2023-06-14T14:29:43.385Z");

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var exerciseId = resultProgramExercise.Id;

            var resultImport = await handlerImportProgramDay.Handle(new ImportProgramDayCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                DiaryId = null,
                Date = dateValue,
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var resultDiaryDays = diary?.DiaryDays.FirstOrDefault(d => d.DiaryId == diary.Id);
            var resultDiaryDaysExercises = resultDiaryDays?.Exercises.FirstOrDefault(d => d.Name == "ExerciseName");

            // Assert
            Assert.Equal(Unit.Value, resultImport);
            Assert.Equal(diary?.Id, resultDiaryDays?.DiaryId);
            Assert.Equal(ExercisePrototypeId_A, resultDiaryDaysExercises?.ExercisePrototypeId);
        }

        [Fact]
        public async Task ImportProgramDayHandler_InSharedDiary_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerCreateDiary = new CreateDiaryHandler(Context, Mapper, FileService);
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerImportProgramDay = new ImportProgramDayHandler(Context, Mapper);
            var handlerAccessToMyDiaryByUsername = new AccessToMyDiaryByUsernameHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var ExercisePrototypeId_A = Guid.NewGuid().ToString();
            var profileId = Guid.NewGuid().ToString();

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

            Context.Profiles.Add(profile);
            await Context.SaveChangesAsync();

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);
            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            user.Username = "user-bill";
            user.IsCoach = true;
            await Context.SaveChangesAsync();

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
                Level = Level.Advanced,
                Type = ProgramType.WeightGain
            }, CancellationToken.None);

            var programId = resultProgram.Id;

            var resultProgramDay = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName",
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
            DateTime dateValue = DateTime.Parse("2023-06-14T14:29:43.385Z");

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var exerciseId = resultProgramExercise.Id;

            await handlerAccessToMyDiaryByUsername.Handle(new AccessToMyDiaryByUsernameCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = "user-chandler"
            }, CancellationToken.None);

            var resultImport = await handlerImportProgramDay.Handle(new ImportProgramDayCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                DiaryId = diary?.Id,
                Date = dateValue,
                UserId = profileId,
            }, CancellationToken.None);

            var resultDiaryDays = diary?.DiaryDays.FirstOrDefault(d => d.DiaryId == diary.Id);
            var resultDiaryDaysExercises = resultDiaryDays?.Exercises?.FirstOrDefault(d => d.Name == "ExerciseName");

            // Assert
            Assert.Equal(Unit.Value, resultImport);
            Assert.Equal(diary?.Id, resultDiaryDays?.DiaryId);
            Assert.Equal(ExercisePrototypeId_A, resultDiaryDaysExercises?.ExercisePrototypeId);
        }
    }
}
