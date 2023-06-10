using AutoMapper;
using Gymby.Application.Mediatr.Statistics.Queries.GetAllNumberStatistics;
using Gymby.Application.Mediatr.Statistics.Queries.GetApproachesDoneCouneByDate;
using Gymby.Application.Mediatr.Statistics.Queries.GetExercisesDoneCountByDate;
using Gymby.UnitTests.Common.Statistics;

namespace Gymby.UnitTests.Mediatr.Statistics.Queries.GetAllNumberStatisticsTests
{
    public class GetAllNumberStatisticsHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetAllNumberStatisticsHandlerTests()
        {
            StatisticCommandTestFixture fixture = new StatisticCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }


        [Fact]
        public async Task GetAllNumberStatisticsHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerGetAllNumberStatistics = new GetAllNumberStatisticsHandler(Context);

            // Act
            var resultGetAllNumberStatisticse = await handlerGetAllNumberStatistics.Handle(new GetAllNumberStatisticsQuery()
            {
                UserId = StatisticContextFactory.UserAId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(resultGetAllNumberStatisticse);
            resultGetAllNumberStatisticse.Should().BeOfType<NumericStatisticsVm>();
            resultGetAllNumberStatisticse.CountOfExecutedApproaches.Should().Be(7);
            resultGetAllNumberStatisticse.MaxApproachesCountPerTraining.Should().Be(70);
            resultGetAllNumberStatisticse.MaxExercisesCountPerTraining.Should().Be(1);
            resultGetAllNumberStatisticse.MaxTonnagePerTraining.Should().Be(1300);
            resultGetAllNumberStatisticse.СountOfTrainings.Should().Be(2);
            resultGetAllNumberStatisticse.СountOfExecutedExercises.Should().Be(2);
        }

        [Fact]
        public async Task GetAllNumberStatisticsHandler_ShouldBeFail()
        {
            // Arrange
            var handlerGetAllNumberStatistics = new GetAllNumberStatisticsHandler(Context);

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerGetAllNumberStatistics.Handle(new GetAllNumberStatisticsQuery()
                {
                    UserId = StatisticContextFactory.UserBId.ToString()
                }, CancellationToken.None);
            });

            Assert.Equal($"Entity \"{StatisticContextFactory.UserBId.ToString()}\" ({nameof(Domain.Entities.DiaryAccess)}) not found", exception.Message);
        }
    }
}
