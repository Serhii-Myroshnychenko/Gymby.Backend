using Gymby.Application.Mediatr.ProgramDays.Commands.DeleteProgramDay;

namespace Gymby.UnitTests.Mediatr.ProgramDays.Commands.DeleteProgramDay
{
    public class DeleteProgramDayCommandTests
    {
        [Fact]
        public void DeleteProgramDayCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var programDayId = "testProgramDayId";

            // Act
            var command = new DeleteProgramDayCommand
            {
                UserId = userId,
                ProgramId = programId,
                ProgramDayId = programDayId,
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(programDayId, command.ProgramDayId);
        }
    }
}
