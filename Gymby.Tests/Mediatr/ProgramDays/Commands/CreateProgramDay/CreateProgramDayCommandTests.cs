using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.ProgramDays.Commands.CreateProgramDay
{
    public class CreateProgramDayCommandTests
    {
        [Fact]
        public void CreateProgramDayCommand_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";
            var name = "testName";

            // Act
            var command = new CreateProgramDayCommand
            {
                UserId = userId,
                ProgramId = programId,
                Name = name
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(programId, command.ProgramId);
            Assert.Equal(name, command.Name);
        }
    }
}
