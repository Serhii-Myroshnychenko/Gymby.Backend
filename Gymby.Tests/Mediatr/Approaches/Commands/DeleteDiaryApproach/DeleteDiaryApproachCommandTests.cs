using Gymby.Application.Mediatr.Approaches.Commands.DeleteDiaryApproach;

namespace Gymby.UnitTests.Mediatr.Approaches.Commands.DeleteDiaryApproach
{
    public class DeleteDiaryApproachCommandTests
    {
        [Fact]
        public void DeleteDiaryApproachCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var approachId = "testApproachId";

            // Act
            var command = new DeleteDiaryApproachCommand
            {
                UserId = userId,
                ApproachId = approachId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(approachId, command.ApproachId);
        }
    }
}
