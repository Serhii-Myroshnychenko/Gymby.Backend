using AutoMapper;
using Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;
using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

namespace Gymby.UnitTests.Mediatr.Measurements.Commands.AddMeasurement
{
    public class AddMeasurementHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public AddMeasurementHandlerTests()
        {
            MeasurementCommandTestFixture fixture = new MeasurementCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task AddMeasurementHandler_FirstCase_ShouldBeSuccess()
        {
            // Arrange
            var handler = new AddMeasurementHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAddPhoto = new AddPhotoHandler(Context, Mapper, FileService);

            var expectedValue = 75.1;
            var UserCId = Guid.NewGuid();

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var appConfigOptionsPhoto = Options.Create(new AppConfig());
            var photoMock = new Mock<IFormFile>();
            photoMock.Setup(p => p.FileName).Returns("path/photoD1.jpg");
            var photo = photoMock.Object;

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = UserCId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var result = await handler.Handle(new AddMeasurementCommand()
            {
                Type = MeasurementType.Shoulders,
                Unit = Units.Cm,
                Value = expectedValue,
                UserId = UserCId.ToString()
            }, CancellationToken.None);

            await handlerAddPhoto.Handle(new AddPhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
            {
                Photo = photo,
                UserId = UserCId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(
               await Context.Measurements.SingleOrDefaultAsync(measurement =>
                    measurement.Unit == Units.Cm &&
                    measurement.Value == expectedValue &&
                    measurement.Type == MeasurementType.Shoulders &&
                    measurement.UserId == UserCId.ToString()));
            Assert.Equal(
                Context.Measurements.FirstOrDefault(measurement =>
                    measurement.UserId == UserCId.ToString())?.UserId,
                Context.Photos.FirstOrDefault(photos =>
                    photos.UserId == UserCId.ToString())?.UserId);
            result.ShouldBeOfType<MeasurementsList>();
        }


        [Fact]
        public async Task AddMeasurementHandler_SecondCase_ShouldBeSuccess()
        {
            // Arrange
            var handler = new AddMeasurementHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAddPhoto = new AddPhotoHandler(Context, Mapper, FileService);

            var expectedValue = 90.2;
            var UserCId = Guid.NewGuid();

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var appConfigOptionsPhoto = Options.Create(new AppConfig());
            var photoMock = new Mock<IFormFile>();
            photoMock.Setup(p => p.FileName).Returns("path/photoD1.jpg");
            var photo = photoMock.Object;

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = UserCId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var result = await handler.Handle(new AddMeasurementCommand()
            {
                Type = MeasurementType.Weight,
                Unit = Units.Kg,
                Value = expectedValue,
                UserId = UserCId.ToString()
            }, CancellationToken.None);

            await handlerAddPhoto.Handle(new AddPhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
            {
                Photo = photo,
                UserId = UserCId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(
               await Context.Measurements.SingleOrDefaultAsync(measurement =>
                    measurement.Unit == Units.Kg &&
                    measurement.Value == expectedValue &&
                    measurement.Type == MeasurementType.Weight &&
                    measurement.UserId == UserCId.ToString()));
            Assert.Equal(
                Context.Measurements.FirstOrDefault(measurement =>
                    measurement.UserId == UserCId.ToString())?.UserId,
                Context.Photos.FirstOrDefault(photos =>
                    photos.UserId == UserCId.ToString())?.UserId);
            result.ShouldBeOfType<MeasurementsList>();
        }
    }
}
