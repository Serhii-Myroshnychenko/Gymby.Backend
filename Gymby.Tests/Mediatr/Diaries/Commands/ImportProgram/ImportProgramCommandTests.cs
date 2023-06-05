using Gymby.Application.Mediatr.Diaries.Command.ImportProgram;

namespace Gymby.UnitTests.Mediatr.Diaries.Commands.ImportProgram
{
    public class ImportProgramCommandTests
    {
        [Fact]
        public void ImportProgramCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var diaryId = "testDiaryId";
            var programId = "testProgramd";
            var daysOfWeek = new List<string> { "Monday", "Sunday", "Saturday" };
            var startDate = DateTime.Now;

            // Act
            var command = new ImportProgramCommand
            {
                UserId = userId,
                DiaryId = diaryId,
                ProgramId = programId,
                DaysOfWeek = daysOfWeek,
                StartDate = startDate
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(diaryId, command.DiaryId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(daysOfWeek, command.DaysOfWeek);
            Assert.Equal(startDate, command.StartDate);
        }
    }
}
