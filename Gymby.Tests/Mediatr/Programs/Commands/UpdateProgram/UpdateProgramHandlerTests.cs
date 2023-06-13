using AutoMapper;
using Gymby.Application.CommandModels.CreateProgramModels;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.Application.Mediatr.Programs.Commands.UpdateProgram;
using Gymby.UnitTests.Common.Programs;

namespace Gymby.UnitTests.Mediatr.Programs.Commands.UpdateProgram
{
    public class UpdateProgramHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public UpdateProgramHandlerTests()
        {
            ProgramCommandTestFixture fixture = new ProgramCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task UpdateProgramHandler_WhenUserOwner_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgramCreate = new CreateProgramHandler(Context, Mapper);
            var handlerProgramUpdate = new UpdateProgramHandler(Context, Mapper);
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
                                            Weight = 20.5
                                        },
                                        new ApproachCM
                                        {
                                            Repeats = 8,
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
            var resultUpdate = await handlerProgramUpdate.Handle(new UpdateProgramCommand()
            {
                ProgramId = programId,
                Description = "Update programmm description",
                Name = "UPDATE",
                Level = "Beginner",
                Type = "Endurance",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(resultCreate);
            Assert.NotNull(
               await Context.Programs.SingleOrDefaultAsync(program =>
                    program.Name == "UPDATE" &&
                    program.Description == "Update programmm description" &&
                    program.Type == ProgramType.Endurance &&
                    program.Level == Level.Beginner &&
                    program.IsPublic == false));
            Assert.Collection(resultCreate.ProgramDays, day =>
            {
                Assert.Equal("Day 1", day.Name);
                Assert.Collection(day.Exercises, exercise =>
                {
                    Assert.Equal("Exercise 1", exercise.Name);
                    Assert.Equal("5224eb66-74df-4632-a43b-eaf561f33319", exercise.ExercisePrototypeId);
                    Assert.Collection(exercise.Approaches, approach =>
                    {
                        Assert.Equal(10, approach.Repeats);
                        Assert.Equal(20.5, approach.Weight);
                    }, approach =>
                    {
                        Assert.Equal(8, approach.Repeats);
                        Assert.Equal(22.5, approach.Weight);
                    });
                }, exercise =>
                {
                    Assert.Equal("Exercise 2", exercise.Name);
                    Assert.Equal("5224eb66-74df-4632-a43b-eaf561f33319", exercise.ExercisePrototypeId);
                    Assert.Collection(exercise.Approaches, approach =>
                    {
                        Assert.Equal(12, approach.Repeats);
                        Assert.Equal(15.0, approach.Weight);
                    });
                });
            });
        }

        [Fact]
        public async Task UpdateProgramHandler_WhenUserNotOwner_ShouldBeFail()
        {
            // Arrange
            var handlerProgramCreate = new CreateProgramHandler(Context, Mapper);
            var handlerProgramUpdate = new UpdateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            
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

            var programId = resultCreate.Id;

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<InsufficientRightsException>(async () =>
            {
                await handlerProgramUpdate.Handle(new UpdateProgramCommand()
                {
                    ProgramId = programId,
                    Description = "Update programmm description",
                    Name = "UPDATE",
                    Level = "Beginner",
                    Type = "Endurance",
                    UserId = ProfileContextFactory.UserAId.ToString()
                }, CancellationToken.None);
            });

            Assert.Equal("You can not modify this program", exception.Message);
        }
    }
}
