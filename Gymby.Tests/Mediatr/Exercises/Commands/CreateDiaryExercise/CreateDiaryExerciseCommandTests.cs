using Gymby.Application.Mediatr.Exercises.Commands.CreateDiaryExercise;

namespace Gymby.UnitTests.Mediatr.Exercises.Commands.CreateDiaryExercise
{
    public class CreateDiaryExerciseCommandTests
    {
        [Fact]
        public void CreateDiaryExerciseCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var name = "testName";
            var programDayId = "testProgramDayId";
            var exercisePrototypeId = "testExercisePrototypeId";
            var diaryId = "testDiaryId";
            var date = DateTime.Now;

            // Act
            var command = new CreateDiaryExerciseCommand
            {
                UserId = userId,
                DiaryId = diaryId,
                Date = date,
                Name = name,
                ProgramDayId = programDayId,
                ExercisePrototypeId = exercisePrototypeId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(diaryId, command.DiaryId);
            Assert.Equal(name, command.Name);
            Assert.Equal(programDayId, command.ProgramDayId);
            Assert.Equal(exercisePrototypeId, command.ExercisePrototypeId);
            Assert.Equal(date, command.Date);
        }
    }
}
