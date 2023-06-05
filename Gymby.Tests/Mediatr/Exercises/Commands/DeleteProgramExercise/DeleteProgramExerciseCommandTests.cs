using Gymby.Application.Mediatr.Exercises.Commands.DeleteProgramExercise;

namespace Gymby.UnitTests.Mediatr.Exercises.Commands.DeleteProgramExercise
{
    public class DeleteProgramExerciseCommandTests
    {
        [Fact]
        public void DeleteProgramExerciseCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var exerciseId = "testExerciseId";

            // Act
            var command = new DeleteProgramExerciseCommand
            {
                UserId = userId,
                ProgramId = programId,
                ExerciseId = exerciseId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(exerciseId, command.ExerciseId);
        }
    }
}
