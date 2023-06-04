using Gymby.Application.Mediatr.Approaches.Commands.UpdateDiaryApproach;

namespace Gymby.UnitTests.Mediatr.Approaches.Commands.UpdateDiaryApproach
{
    public class UpdateDiaryApproachCommandTests
    {
        [Fact]
        public void UpdateDiaryApproachCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var exerciseId = "testExerciseId";
            var approachId = "testApproachId";
            var repeats = 1;
            var weight = 11;
            var isDone = true;

            // Act
            var command = new UpdateDiaryApproachCommand
            {
                UserId = userId,
                ExerciseId = exerciseId,
                Repeats = repeats,
                Weight = weight,
                IsDone = isDone,
                ApproachId = approachId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(exerciseId, command.ExerciseId);
            Assert.Equal(repeats, command.Repeats);
            Assert.Equal(weight, command.Weight);
            Assert.Equal(isDone, command.IsDone);
            Assert.Equal(approachId, command.ApproachId);
        }
    }
}
