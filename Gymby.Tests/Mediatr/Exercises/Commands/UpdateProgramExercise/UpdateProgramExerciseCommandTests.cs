using Gymby.Application.Mediatr.Exercises.Commands.UpdateProgramExercise;

namespace Gymby.UnitTests.Mediatr.Exercises.Commands.UpdateProgramExercise
{
    public class UpdateProgramExerciseCommandTests
    {
        [Fact]
        public void UpdateProgramExerciseCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var name = "testName";
            var exercisePrototypeId = "testExercisePrototypeId";
            var exerciseId = "testExerciseId";

            // Act
            var command = new UpdateProgramExerciseCommand
            {
                UserId = userId,
                ProgramId = programId,
                Name = name,
                ExerciseId = exerciseId,
                ExercisePrototypeId = exercisePrototypeId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(name, command.Name);
            Assert.Equal(exerciseId, command.ExerciseId);
            Assert.Equal(exercisePrototypeId, command.ExercisePrototypeId);
        }
    }
}
