using Gymby.Application.Mediatr.Approaches.Commands.UpdateProgramApproach;

namespace Gymby.UnitTests.Mediatr.Approaches.Commands.UpdateProgramApproach
{
    public class UpdateProgramApproachCommandTests
    {
        [Fact]
        public void UpdateProgramApproachCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var exerciseId = "testExerciseId";
            var isDone = false;
            var repeats = 1;
            var weight = 1;

            // Act
            var command = new UpdateProgramApproachCommand
            {
                UserId = userId,
                ProgramId = programId,
                ExerciseId = exerciseId,
                IsDone = isDone,
                Repeats = repeats,
                Weight = weight
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(exerciseId, command.ExerciseId);
            Assert.Equal(isDone, command.IsDone);
            Assert.Equal(repeats, command.Repeats);
            Assert.Equal(weight, command.Weight);
        }
    }
}
