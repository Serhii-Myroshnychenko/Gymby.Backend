﻿using AutoMapper;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.Application.Mediatr.Exercises.Commands.CreateDiaryExercise;
using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;
using Gymby.Application.Mediatr.Exercises.Commands.DeleteDiaryExercise;
using Gymby.Application.Mediatr.Exercises.Commands.UpdateDiaryExercise;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Exercise;

namespace Gymby.UnitTests.Mediatr.Exercises.Commands.DeleteDiaryExercise
{
    public class DeleteDiaryExerciseHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public DeleteDiaryExerciseHandlerTests()
        {
            ProgramExerciseCommandTestFixture fixture = new ProgramExerciseCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task DeleteDiaryExerciseHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerDiaryExerciseCreate = new CreateDiaryExerciseHandler(Context, Mapper);
            var handlerDiaryExerciseUpdate = new UpdateDiaryExerciseHandler(Context, Mapper);
            var handlerDiaryExerciseDelete = new DeleteDiaryExerciseHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            var ExercisePrototypeId_A = Guid.NewGuid().ToString();

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

            var resultDiaryExercise = await handlerDiaryExerciseCreate.Handle(new CreateDiaryExerciseCommand()
            {
                ProgramDayId = programDayId,
                DiaryId = diaryId,
                Name = "ExerciseNameInDiary1",
                Date = dateValue,
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var resultDiaryExerciseId = resultDiaryExercise.Id;

            var resultDiaryExerciseDelete = await handlerDiaryExerciseDelete.Handle(new DeleteDiaryExerciseCommand()
            {
                ExerciseId = resultDiaryExerciseId,
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, resultDiaryExerciseDelete);
        }

        [Fact]
        public async Task DeleteDiaryExerciseHandler_ShouldBeFail()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerDiaryExerciseCreate = new CreateDiaryExerciseHandler(Context, Mapper);
            var handlerDiaryExerciseUpdate = new UpdateDiaryExerciseHandler(Context, Mapper);
            var handlerDiaryExerciseDelete = new DeleteDiaryExerciseHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            var ExercisePrototypeId_A = Guid.NewGuid().ToString();
            var exerciseId = Guid.NewGuid().ToString();

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

            var resultDiaryExercise = await handlerDiaryExerciseCreate.Handle(new CreateDiaryExerciseCommand()
            {
                ProgramDayId = programDayId,
                DiaryId = diaryId,
                Name = "ExerciseNameInDiary1",
                Date = dateValue,
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerDiaryExerciseDelete.Handle(new DeleteDiaryExerciseCommand()
                {
                    ExerciseId = exerciseId,
                    UserId = ProfileContextFactory.UserBId.ToString()
                }, CancellationToken.None);
            });

            Assert.Equal($"Entity \"{exerciseId}\" ({nameof(Domain.Entities.Exercise)}) not found", exception.Message);
        }
    }
}