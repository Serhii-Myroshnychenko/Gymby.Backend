using Gymby.Application.CommandModels.CreateProgramModels;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;

namespace Gymby.UnitTests.Mediatr.Programs.Commands.CreateProgram
{
    public class CreateProgramCommandTests
    {
        [Fact]
        public void CreateProgramCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "user1";
            var name = "Program 1";
            var description = "Sample program";
            var level = "Beginner";
            var type = "WeightLoss";
            var programDays = new List<ProgramDayCM>
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
                       }
            };

            // Act
            var command = new CreateProgramCommand
            {
                UserId = userId,
                Name = name,
                Description = description,
                Level = level,
                Type = type,
                ProgramDays = programDays
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(name, command.Name);
            Assert.Equal(description, command.Description);
            Assert.Equal(level, command.Level);
            Assert.Equal(type, command.Type);
            Assert.Equal(programDays, command.ProgramDays);
        }
    }
}
