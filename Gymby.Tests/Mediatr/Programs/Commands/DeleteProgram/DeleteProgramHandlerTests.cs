using AutoMapper;
using Gymby.Application.CommandModels.CreateProgramModels;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.Application.Mediatr.Programs.Commands.DeleteProgram;
using Gymby.UnitTests.Common.Programs;

namespace Gymby.UnitTests.Mediatr.Programs.Commands.DeleteProgram
{
    public class DeleteProgramHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public DeleteProgramHandlerTests()
        {
            ProgramCommandTestFixture fixture = new ProgramCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task DeleteProgramHandler_WhenUserOwner_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgramCreate = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDelete = new DeleteProgramHandler(Context);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var resultCreate = await handlerProgramCreate.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "ProgramName1",
                Description = "Description1",
                Level = "Advanced",
                Type = "WeightGain",
                ProgramDays = new List<ProgramDayCM>
                    {
                       new ProgramDayCM
                       {
                            Name = "Day 1",
                            Exercises = new List<ExerciseCM>
                            {
                                new ExerciseCM
                                {
                                    Name = "Exercise 1",
                                    ExercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                                    Approaches = new List<ApproachCM>
                                    {
                                        new ApproachCM
                                        {
                                            Repeats = 10,
                                            Interval = 60,
                                            Weight = 20.5
                                        },
                                        new ApproachCM
                                        {
                                            Repeats = 8,
                                            Interval = 60,
                                            Weight = 22.5
                                        }
                                    }
                                },
                                new ExerciseCM
                                {
                                    Name = "Exercise 2",
                                    ExercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                                    Approaches = new List<ApproachCM>
                                    {
                                        new ApproachCM
                                        {
                                            Repeats = 12,
                                            Interval = 60,
                                            Weight = 15.0
                                        }
                                    }
                                }
                            }
                       },
                }
            }, CancellationToken.None);

            var programId = resultCreate.Id;

            // Act
            var resultDelete = await handlerProgramDelete.Handle(new DeleteProgramCommand()
            {
                ProgramId = programId,
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, resultDelete);
            var deletedProgram = await Context.Programs.FindAsync(programId);
            Assert.Null(deletedProgram);
        }

        [Fact]
        public async Task DeleteProgramHandler_WhenIncorrectProgramId_ShouldBeFail()
        {
            // Arrange
            var handlerProgramCreate = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDelete = new DeleteProgramHandler(Context);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var programId = Guid.NewGuid().ToString();

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Email = "user-c@gmail.com"
            }, CancellationToken.None);

            var resultCreate = await handlerProgramCreate.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "ProgramName1",
                Description = "Description1",
                Level = "Advanced",
                Type = "WeightGain"
            }, CancellationToken.None);

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerProgramDelete.Handle(new DeleteProgramCommand()
                {
                    ProgramId = programId,
                    UserId = ProfileContextFactory.UserBId.ToString()
                }, CancellationToken.None);
            });

            Assert.Equal($"Entity \"{programId}\" ({nameof(Domain.Entities.Program)}) not found", exception.Message);
        }
    }
}
