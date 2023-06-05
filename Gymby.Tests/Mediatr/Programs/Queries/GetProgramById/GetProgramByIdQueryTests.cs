using Gymby.Application.Mediatr.Programs.Queries.GetProgramById;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.GetProgramById
{
    public class GetProgramByIdQueryTests
    {
        [Fact]
        public void GetProgramByIdQuery_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var userId = "testUserId";
            var programId = "testProgramId";

            // Act
            var command = new GetProgramByIdQuery
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
