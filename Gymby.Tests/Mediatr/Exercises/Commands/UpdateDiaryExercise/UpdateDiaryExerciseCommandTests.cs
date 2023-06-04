using Gymby.Application.Mediatr.Exercises.Commands.UpdateDiaryExercise;

namespace Gymby.UnitTests.Mediatr.Exercises.Commands.UpdateDiaryExercise
{
    public class UpdateDiaryExerciseCommandTests
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
            var command = new UpdateDiaryExerciseCommand
            {
                UserId = userId,
                Name = name,
                ExerciseId = exerciseId,
                ExercisePrototypeId = exercisePrototypeId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(exerciseId, command.ExerciseId);
            Assert.Equal(name, command.Name);
            Assert.Equal(exercisePrototypeId, command.ExercisePrototypeId);
        }
    }
}
