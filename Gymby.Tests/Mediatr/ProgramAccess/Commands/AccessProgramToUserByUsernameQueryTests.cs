using Gymby.Application.Mediatr.ProgramAccesses.AccessProgramToUserByUsername;

namespace Gymby.UnitTests.Mediatr.ProgramAccess.Commands
{
    public class AccessProgramToUserByUsernameQueryTests
    {
        [Fact]
        public void AccessProgramToUserByUsernameQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var username = "testUsername";

            // Act
            var command = new AccessProgramToUserByUsernameQuery
            {
                UserId = userId,
                ProgramId = programId,
                Username = username,
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(username, command.Username);
        }
    }
}
