using AutoMapper;
using Gymby.Application.Mediatr.Approaches.Commands.CreateProgramApproach;
using Gymby.Application.Mediatr.Approaches.Commands.DeleteProgramApproach;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Exercise;

namespace Gymby.UnitTests.Mediatr.Approaches.Commands.DeleteProgramApproach
{
    public class DeleteProgramApproachHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public DeleteProgramApproachHandlerTests()
        {
            ProgramExerciseCommandTestFixture fixture = new ProgramExerciseCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task DeleteProgramApproachHandler_WhenUserCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerApproachCreate = new CreateProgramApproachHandler(Context, Mapper, FileService);
            var handlerApproachDelete = new DeleteProgramApproachHandler(Context, Mapper, FileService);

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

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var resultProgramExerciseId = resultProgramExercise.Id;

            var resultProgramApproach = await handlerApproachCreate.Handle(new CreateProgramApproachCommand()
            {
                ProgramId = programId,
                ExerciseId = resultProgramExerciseId,
                Weight = 30,
                Repeats = 15,
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var approachId = resultProgramApproach?.Approaches?.FirstOrDefault()?.Id;

            var resultProgramApproachDelete = await handlerApproachDelete.Handle(new DeleteProgramApproachCommand()
            {
                ExerciseId = resultProgramExerciseId,
                ProgramId = programId,
                UserId = ProfileContextFactory.UserBId.ToString(),
                ApproachId = approachId ?? ""
            }, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, resultProgramApproachDelete);
        }

        [Fact]
        public async Task DeleteProgramApproachHandler_WhenUserNotCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerApproachCreate = new CreateProgramApproachHandler(Context, Mapper, FileService);
            var handlerApproachDelete = new DeleteProgramApproachHandler(Context, Mapper, FileService);

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

            var resultProgramExercise = await handlerProgramExercise.Handle(new CreateProgramExerciseCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ExerciseName",
                UserId = ProfileContextFactory.UserBId.ToString(),
                ExercisePrototypeId = exercisePrototype
            }, CancellationToken.None);

            var resultProgramExerciseId = resultProgramExercise.Id;

            var resultProgramApproach = await handlerApproachCreate.Handle(new CreateProgramApproachCommand()
            {
                ProgramId = programId,
                ExerciseId = resultProgramExerciseId,
                Weight = 30,
                Repeats = 15,
                UserId = ProfileContextFactory.UserBId.ToString(),
            }, CancellationToken.None);

            var approachId = resultProgramApproach?.Approaches?.FirstOrDefault()?.Id;

            //Assert
            var exception = await Assert.ThrowsAsync<InsufficientRightsException>(async () =>
            {
                await handlerApproachDelete.Handle(new DeleteProgramApproachCommand()
                {
                    ExerciseId = resultProgramExerciseId,
                    ProgramId = programId,
                    UserId = ProfileContextFactory.UserAId.ToString(),
                    ApproachId = approachId ?? ""
                }, CancellationToken.None);
            });

            Assert.Equal("You do not have permissions to delete an approach", exception.Message);
        }
    }
}
