using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;

namespace Gymby.UnitTests.Mediatr.ExercisePrototype.Queries
{
    public class GetAllExercisePrototypesQueryTests
    {
        [Fact]
        public void GetAllExercisePrototypesQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";

            // Act
            var command = new GetAllExercisePrototypesQuery
            {
                UserId = userId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
        }
    }
}
