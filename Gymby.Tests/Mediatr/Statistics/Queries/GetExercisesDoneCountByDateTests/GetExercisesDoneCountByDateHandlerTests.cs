using AutoMapper;
using Gymby.Application.Mediatr.Statistics.Queries.GetExercisesDoneCountByDate;
using Gymby.UnitTests.Common.Statistics;

namespace Gymby.UnitTests.Mediatr.Statistics.Queries.GetExercisesDoneCountByDateTests
{
    public class GetExercisesDoneCountByDateHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetExercisesDoneCountByDateHandlerTests()
        {
            StatisticCommandTestFixture fixture = new StatisticCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetExercisesDoneCountByDateHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerGetExercisesDoneCountByDate = new GetExercisesDoneCountByDateHandler(Context, Mapper);

            // Act
            var resultGetExercisesDoneCountByDate = await handlerGetExercisesDoneCountByDate.Handle(new GetExercisesDoneCountByDateQuery()
            {
                UserId = StatisticContextFactory.UserAId.ToString(),
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(2),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(resultGetExercisesDoneCountByDate);
            resultGetExercisesDoneCountByDate.Count.Should().Be(2);
            Assert.Equal(1, resultGetExercisesDoneCountByDate[0].Value);
            Assert.Equal(1, resultGetExercisesDoneCountByDate[1].Value);
        }

        [Fact]
        public async Task GetExercisesDoneCountByDateHandler_ShouldBeFail()
        {
            // Arrange
            var handlerGetExercisesDoneCountByDate = new GetExercisesDoneCountByDateHandler(Context, Mapper);

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerGetExercisesDoneCountByDate.Handle(new GetExercisesDoneCountByDateQuery()
                {
                    UserId = StatisticContextFactory.UserBId.ToString(),
                    StartDate = DateTime.Now.AddDays(-2),
                    EndDate = DateTime.Now.AddDays(2),
                }, CancellationToken.None);
            });

            Assert.Equal($"Entity \"{StatisticContextFactory.UserBId.ToString()}\" ({nameof(Domain.Entities.DiaryAccess)}) not found", exception.Message);
        }
    }
}
