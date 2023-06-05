using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;

namespace Gymby.UnitTests.Mediatr.Exercises.Commands.CreateProgramExercise
{
    public class CreateProgramExerciseCommandTests
    {
        [Fact]
        public void CreateProgramExerciseCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var name = "testName";
            var programDayId = "testProgramDayId";
            var exercisePrototypeId = "testExercisePrototypeId";

            // Act
            var command = new CreateProgramExerciseCommand
            {
                UserId = userId,
                ProgramId = programId,
                Name = name,
                ProgramDayId = programDayId,
                ExercisePrototypeId = exercisePrototypeId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(name, command.Name);
            Assert.Equal(programDayId, command.ProgramDayId);
            Assert.Equal(exercisePrototypeId, command.ExercisePrototypeId);
        }
    }
}
