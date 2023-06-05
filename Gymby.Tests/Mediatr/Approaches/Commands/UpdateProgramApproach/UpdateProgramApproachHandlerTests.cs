using AutoMapper;
using Gymby.Application.Mediatr.Approaches.Commands.CreateProgramApproach;
using Gymby.Application.Mediatr.Approaches.Commands.UpdateProgramApproach;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;
using Gymby.Application.Mediatr.Exercises.Commands.UpdateProgramExercise;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Exercise;

namespace Gymby.UnitTests.Mediatr.Approaches.Commands.UpdateProgramApproach
{
    public class UpdateProgramApproachHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public UpdateProgramApproachHandlerTests()
        {
            ProgramExerciseCommandTestFixture fixture = new ProgramExerciseCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task UpdateProgramApproachHandler_WhenUserCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerApproachCreate = new CreateProgramApproachHandler(Context, Mapper, FileService);
            var handlerApproachUpdate = new UpdateProgramApproachHandler(Context, Mapper, FileService);

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

            var resultProgramApproachUpdate = await handlerApproachUpdate.Handle(new UpdateProgramApproachCommand()
            {
                ExerciseId = resultProgramExerciseId,
                ProgramId = programId,
                UserId = ProfileContextFactory.UserBId.ToString(),
                Repeats = 1,
                Weight = 1,
                ApproachId = approachId ?? ""
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(resultProgramApproachUpdate);
            Assert.Equal(approachId, resultProgramApproachUpdate?.Approaches?.FirstOrDefault()?.Id);
            Assert.Equal(1, resultProgramApproachUpdate?.Approaches?.FirstOrDefault()?.Repeats);
            Assert.Equal(1, resultProgramApproachUpdate?.Approaches?.FirstOrDefault()?.Weight);
            Assert.Equal(programDayId, resultProgramApproachUpdate?.ProgramDayId);
        }

        [Fact]
        public async Task UpdateProgramApproachHandler_WhenUserNotCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerExerciseUpdate = new UpdateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerApproachCreate = new CreateProgramApproachHandler(Context, Mapper, FileService);
            var handlerApproachUpdate = new UpdateProgramApproachHandler(Context, Mapper, FileService);

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
                await handlerApproachUpdate.Handle(new UpdateProgramApproachCommand()
                {
                    ExerciseId = resultProgramExerciseId,
                    ProgramId = programId,
                    UserId = ProfileContextFactory.UserAId.ToString(),
                    Repeats = 1,
                    Weight = 1,
                    ApproachId = approachId ?? ""
                }, CancellationToken.None);
            });

            Assert.Equal("You do not have permissions to update an approach", exception.Message);
        }

        [Fact]
        public async Task UpdateProgramApproachHandler_WhenNonexistentExercise_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerExerciseUpdate = new UpdateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerApproachCreate = new CreateProgramApproachHandler(Context, Mapper, FileService);
            var handlerApproachUpdate = new UpdateProgramApproachHandler(Context, Mapper, FileService);

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
            var exerciseId = Guid.NewGuid().ToString();

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerApproachUpdate.Handle(new UpdateProgramApproachCommand()
                {
                    ExerciseId = exerciseId,
                    ProgramId = programId,
                    UserId = ProfileContextFactory.UserBId.ToString(),
                    Repeats = 1,
                    Weight = 1,
                    ApproachId = approachId ?? ""
                }, CancellationToken.None);
            });

            Assert.Equal($"Entity \"{exerciseId}\" ({nameof(Domain.Entities.Exercise)}) not found", exception.Message);
        }

        [Fact]
        public async Task UpdateProgramApproachHandler_WhenNonexistentApproach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramExercise = new CreateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerExercisePrototype = new GetAllExercisePrototypesHandler(Context, Mapper, FileService);
            var handlerExerciseUpdate = new UpdateProgramExerciseHandler(Context, Mapper, FileService);
            var handlerApproachCreate = new CreateProgramApproachHandler(Context, Mapper, FileService);
            var handlerApproachUpdate = new UpdateProgramApproachHandler(Context, Mapper, FileService);

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

            var approachId = Guid.NewGuid().ToString();

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerApproachUpdate.Handle(new UpdateProgramApproachCommand()
                {
                    ExerciseId = resultProgramExerciseId,
                    ProgramId = programId,
                    UserId = ProfileContextFactory.UserBId.ToString(),
                    Repeats = 1,
                    Weight = 1,
                    ApproachId = approachId ?? ""
                }, CancellationToken.None);
            });

            Assert.Equal($"Entity \"{approachId}\" ({nameof(Domain.Entities.Approach)}) not found", exception.Message);
        }
    }
}
