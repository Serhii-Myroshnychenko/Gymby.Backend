using Gymby.Application.Mediatr.Approaches.Commands.CreateProgramApproach;

namespace Gymby.UnitTests.Mediatr.Approaches.Commands.CreateProgramApproach
{
    public class CreateProgramApproachCommandTests
    {
        [Fact]
        public void CreateProgramApproachCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var exerciseId = "testExerciseId";
            // Act
            var command = new CreateProgramApproachCommand
            {
                UserId = userId,
                ProgramId = programId,
                ExerciseId = exerciseId,
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(exerciseId, command.ExerciseId);
        }
    }
}
