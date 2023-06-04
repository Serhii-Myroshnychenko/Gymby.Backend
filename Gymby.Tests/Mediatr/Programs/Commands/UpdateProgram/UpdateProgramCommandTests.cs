using Gymby.Application.Mediatr.Programs.Commands.UpdateProgram;

namespace Gymby.UnitTests.Mediatr.Programs.Commands.UpdateProgram
{
    public class UpdateProgramCommandTests
    {
        public void CreateProgramCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "user1";
            var name = "Program 1";
            var description = "Sample program";
            var level = "Beginner";
            var type = "WeightLoss";
            var programId = "testProgramId";

            // Act
            var command = new UpdateProgramCommand
            {
                UserId = userId,
                Name = name,
                Description = description,
                Level = level,
                Type = type,
                ProgramId = programId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(name, command.Name);
            Assert.Equal(description, command.Description);
            Assert.Equal(level, command.Level);
            Assert.Equal(type, command.Type);
            Assert.Equal(programId, command.ProgramId);
        }
    }
}
