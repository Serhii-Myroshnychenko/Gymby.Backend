using Gymby.Application.Mediatr.Approaches.Commands.DeleteProgramApproach;

namespace Gymby.UnitTests.Mediatr.Approaches.Commands.DeleteProgramApproach
{
    public class DeleteProgramApproachCommandTests
    {
        [Fact]
        public void DeleteProgramApproachCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var exerciseId = "testExerciseId";
            var approachId = "testApproachId";

            // Act
            var command = new DeleteProgramApproachCommand
            {
                UserId = userId,
                ProgramId = programId,
                ExerciseId = exerciseId,
                ApproachId = approachId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(exerciseId, command.ExerciseId);
            Assert.Equal(approachId, command.ApproachId);
        }
    }
}
