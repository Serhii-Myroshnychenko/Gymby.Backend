using Gymby.Application.Mediatr.Exercises.Commands.DeleteDiaryExercise;

namespace Gymby.UnitTests.Mediatr.Exercises.Commands.DeleteDiaryExercise
{
    public class DeleteDiaryExerciseCommandTests
    {
        [Fact]
        public void CreateDiaryExerciseCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var name = "testName";
            var exercisePrototypeId = "testExercisePrototypeId";
            var exerciseId = "testExerciseId";

            // Act
            var command = new DeleteDiaryExerciseCommand
            {
                UserId = userId,
                ExerciseId = exerciseId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(exerciseId, command.ExerciseId);
        }
    }
}
