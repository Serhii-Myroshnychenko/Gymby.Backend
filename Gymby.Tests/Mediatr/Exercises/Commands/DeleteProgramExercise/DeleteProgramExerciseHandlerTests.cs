using AutoMapper;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;
using Gymby.Application.Mediatr.Exercises.Commands.DeleteProgramExercise;
using Gymby.Application.Mediatr.Exercises.Commands.UpdateProgramExercise;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Exercise;

namespace Gymby.UnitTests.Mediatr.Exercises.Commands.DeleteProgramExercise
{
    public class DeleteProgramExerciseHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public DeleteProgramExerciseHandlerTests()
        {
            ProgramExerciseCommandTestFixture fixture = new ProgramExerciseCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task DeleteProgramExerciseHandler_WhenUserCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerExerciseDelete = new DeleteProgramExerciseHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var appConfigOptionsProfile1 = Options.Create(new AppConfig());

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
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

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

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var resultProgramExerciseId = resultProgramExercise.Id;

            var resultProgramExerciseDelete = await handlerExerciseDelete.Handle(new DeleteProgramExerciseCommand()
            {
                ExerciseId = resultProgramExerciseId,
                ProgramId = programId,
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, resultProgramExerciseDelete);
        }

        [Fact]
        public async Task DeleteProgramExerciseHandler_WhenUserNotCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerExerciseDelete = new DeleteProgramExerciseHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var appConfigOptionsProfile1 = Options.Create(new AppConfig());

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
                user.IsCoach = true;
                await Context.SaveChangesAsync();
            }

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

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var resultProgramExerciseId = resultProgramExercise.Id;

            //Assert
            var exception = await Assert.ThrowsAsync<InsufficientRightsException>(async () =>
            {
                await handlerExerciseDelete.Handle(new DeleteProgramExerciseCommand()
                {
                    ExerciseId = resultProgramExerciseId,
                    ProgramId = programId,
                    UserId = ProfileContextFactory.UserAId.ToString(),
                }, CancellationToken.None);
            });

            Assert.Equal("You do not have permissions to delete an exercise", exception.Message);
        }
    }
}
