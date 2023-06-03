using AutoMapper;
using Gymby.Application.CommandModels.CreateProgramModels;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Programs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Programs.Commands.CreateProgram
{
    public class CreateProgramHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public CreateProgramHandlerTests()
        {
            ProgramCommandTestFixture fixture = new ProgramCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task CreateProgramHandler_WhenUserCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var result = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "ProgramName1",
                Description = "Description1",
                Level = Level.Advanced,
                Type = ProgramType.WeightGain,
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

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(
               await Context.Programs.SingleOrDefaultAsync(program =>
                    program.Name == "ProgramName1" &&
                    program.Description == "Description1" &&
                    program.Type == ProgramType.WeightGain &&
                    program.Level == Level.Advanced &&
                    program.IsPublic == false));
            Assert.Collection(result.ProgramDays, day =>
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
        public async Task CreateProgramHandler_WhenUserNotCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            //Assert
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Email = "user-c@gmail.com"
            }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<InsufficientRightsException>(async () =>
            {
                await handlerProgram.Handle(new CreateProgramCommand()
                {
                    UserId = ProfileContextFactory.UserAId.ToString(),
                    Name = "ProgramName1",
                    Description = "Description1",
                    Level = Level.Advanced,
                    Type = ProgramType.WeightGain
                }, CancellationToken.None);
            });

            Assert.Equal("You do not have permissions to create a program", exception.Message);
        }
    }
}
