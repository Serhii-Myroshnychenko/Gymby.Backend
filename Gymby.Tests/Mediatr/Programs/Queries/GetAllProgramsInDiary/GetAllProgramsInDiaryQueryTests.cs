using Gymby.Application.Mediatr.Programs.Queries.GetFreePrograms;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.GetAllProgramsInDiary
{
    public class GetAllProgramsInDiaryQueryTests
    {
        [Fact]
        public void GetAllProgramsInDiaryQuery_ShouldSetProperties()
        {
            // Arrange
            var userId = "testUserId";

            // Act
            var command = new GetFreeProgramsQuery
            {
                UserId = userId
            };

            // Assert
            Assert.Equal(userId, command.UserId);
        }
    }
}
