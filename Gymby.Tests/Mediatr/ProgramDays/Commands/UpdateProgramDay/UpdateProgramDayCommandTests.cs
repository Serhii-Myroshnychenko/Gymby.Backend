using Gymby.Application.Mediatr.ProgramDays.Commands.UpdateProgramDay;

namespace Gymby.UnitTests.Mediatr.ProgramDays.Commands.UpdateProgramDay
{
    public class UpdateProgramDayCommandTests
    {
        [Fact]
        public void UpdateProgramDayCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var name = "testName";
            var programDayId = "testProgramDayId";

            // Act
            var command = new UpdateProgramDayCommand
            {
                UserId = userId,
                ProgramId = programId,
                Name = name,
                ProgramDayId = programDayId,
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(name, command.Name);
            Assert.Equal(programDayId, command.ProgramDayId);
        }
    }
}
