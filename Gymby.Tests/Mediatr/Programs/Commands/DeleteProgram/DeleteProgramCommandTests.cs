using Gymby.Application.Mediatr.Programs.Commands.DeleteProgram;

namespace Gymby.UnitTests.Mediatr.Programs.Commands.DeleteProgram
{
    public class DeleteProgramCommandTests
    {
        public void DeleteProgramCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "user1";
            var programId = "testProgramId";

            // Act
            var command = new DeleteProgramCommand
            {
                UserId = userId,
                ProgramId = programId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
        }
    }
}
