using AutoMapper;
using Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;
using Gymby.Application.Mediatr.Measurements.Commands.DeleteMeasurement;
using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

namespace Gymby.UnitTests.Mediatr.Measurements.Commands.DeleteMeasurement
{
    public class DeleteMeasurementHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public DeleteMeasurementHandlerTests()
        {
            MeasurementCommandTestFixture fixture = new MeasurementCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task DeleteMeasurementHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerAddMeasurement = new AddMeasurementHandler(Context, Mapper, FileService);
            var handlerDeleteMeasurement = new DeleteMeasurementHandler(Context, FileService, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAddPhoto = new AddPhotoHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var appConfigOptionsMeasurement = Options.Create(new AppConfig());
            var appConfigOptionsPhoto = Options.Create(new AppConfig());
            var photoMock = new Mock<IFormFile>();
            photoMock.Setup(p => p.FileName).Returns("path/photoD1.jpg");
            var photo = photoMock.Object;

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var addMeasurementResult = await handlerAddMeasurement.Handle(new AddMeasurementCommand()
            {
                Type = MeasurementType.Shoulders,
                Unit = Units.Cm,
                Value = 75.1,
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var measurementId = addMeasurementResult.Measurements.First().Id;

            var addPhotoResult = await handlerAddPhoto.Handle(new AddPhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
            {
                Photo = photo,
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var photoId = addPhotoResult.First().Id;

            // Act
            await handlerDeleteMeasurement.Handle(new DeleteMeasurementCommand(appConfigOptionsMeasurement)
            {
                Id = measurementId,
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.Null(Context.Measurements.SingleOrDefault(measurement =>
               measurement.Id == measurementId));
        }
    }
}
