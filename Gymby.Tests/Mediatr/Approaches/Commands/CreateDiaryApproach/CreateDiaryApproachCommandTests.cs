using Gymby.Application.Mediatr.Approaches.Commands.CreateDiaryApproach;

namespace Gymby.UnitTests.Mediatr.Approaches.Commands.CreateDiaryApproach
{
    public class CreateDiaryApproachCommandTests
    {
        [Fact]
        public void CreateDiaryApproachCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var exerciseId = "testExerciseId";
            var repeats = 1;
            var weight = 11;

            // Act
            var command = new CreateDiaryApproachCommand
            {
                UserId = userId,
                ExerciseId = exerciseId,
                Repeats = repeats,
                Weight = weight
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(exerciseId, command.ExerciseId);
            Assert.Equal(repeats, command.Repeats);
            Assert.Equal(weight, command.Weight);
        }
    }
}
