using AutoMapper;
using Gymby.Application.Mediatr.Measurements.Queries.GetMyMeasurements;

namespace Gymby.UnitTests.Mediatr.Measurements.Queries.GetMyMeasurements
{
    public class GetMyMeasurementsHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetMyMeasurementsHandlerTests()
        {
            MeasurementCommandTestFixture fixture = new MeasurementCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetMyMeasurementsHandler_ShouldGetMeasurementList()
        {
            // Arrange
            var handler = new GetMyMeasurementsHandler(Context, Mapper, FileService);
            var appConfigOptions = Options.Create(new AppConfig());

            // Act
            var result = await handler.Handle(
                new GetMyMeasurementsQuery(appConfigOptions)
                {
                    UserId = MeasurementContextFactory.UserBId.ToString(),

                }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeOfType<MeasurementsList>();
            result.Measurements.Count().ShouldBe(2);

            foreach (var measurement in result.Measurements)
            {
                measurement.ShouldBeOfType<MeasurementVm>();
                measurement.Id.ShouldNotBeNull();
                measurement.UserId.ShouldNotBeNull().ShouldBe(MeasurementContextFactory.UserBId.ToString());
                measurement.Date.ShouldBeOfType<DateTime>();
                measurement.Type.ShouldBeOfType<MeasurementType>();
                measurement.Value.ShouldBeOfType<double>();
                measurement.Unit.ShouldBeOfType<Units>();
            }
        }

        //[Fact]
        //public async Task GetMyMeasurementsHandler_ShouldBeFailOnWrongUserId()
        //{
        //    // Arrange
        //    var handler = new GetMyMeasurementsHandler(Context, Mapper, FileService);
        //    var appConfigOptions = Options.Create(new AppConfig());

        //    // Act
        //    // Assert
        //    await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
        //       await handler.Handle(
        //       new GetMyMeasurementsQuery(appConfigOptions)
        //       {
        //           UserId = Guid.NewGuid().ToString(),
        //       }, CancellationToken.None));
        //}
    }
}
