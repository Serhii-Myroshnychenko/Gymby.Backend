using Gymby.Application.Mediatr.Diaries.Command.ImportProgramDay;

namespace Gymby.UnitTests.Mediatr.Diaries.Commands.ImportProgramDay
{
    public class ImportProgramDayCommandTests
    {
        [Fact]
        public void ImportProgramDayCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var diaryId = "testDiaryId";
            var programId = "testProgramd";
            var programDayId = "testProgramDayId";
            var date = DateTime.Now;

            // Act
            var command = new ImportProgramDayCommand
            {
                UserId = userId,
                DiaryId = diaryId,
                ProgramId = programId,
                ProgramDayId = programDayId,
                Date = date
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(diaryId, command.DiaryId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(programDayId, command.ProgramDayId);
            Assert.Equal(date, command.Date);
        }
    }
}
