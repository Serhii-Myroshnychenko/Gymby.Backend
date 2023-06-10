using AutoMapper;
using Gymby.Application.Mediatr.Statistics.Queries.GetApproachesDoneCouneByDate;
using Gymby.UnitTests.Common.Statistics;

namespace Gymby.UnitTests.Mediatr.Statistics.Queries.GetApproachesDoneCountByDateTests
{
    public class GetApproachesDoneCountByDateHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetApproachesDoneCountByDateHandlerTests()
        {
            StatisticCommandTestFixture fixture = new StatisticCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetApproachesDoneCountByDateHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerGetApproachesDoneCountByDate = new GetApproachesDoneCountByDateHandler(Context);

            // Act
            var resultGetApproachesDoneCountByDate = await handlerGetApproachesDoneCountByDate.Handle(new GetApproachesDoneCountByDateQuery()
            {
                UserId = StatisticContextFactory.UserAId.ToString(),
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(2),
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(resultGetApproachesDoneCountByDate);
            resultGetApproachesDoneCountByDate.Count.Should().Be(2);
            Assert.Equal(3, resultGetApproachesDoneCountByDate[0].Value);
            Assert.Equal(4, resultGetApproachesDoneCountByDate[1].Value);
        }

        [Fact]
        public async Task GetApproachesDoneCountByDateHandler_ShouldBeFail()
        {
            // Arrange
            var handlerGetApproachesDoneCountByDate = new GetApproachesDoneCountByDateHandler(Context);

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerGetApproachesDoneCountByDate.Handle(new GetApproachesDoneCountByDateQuery()
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
