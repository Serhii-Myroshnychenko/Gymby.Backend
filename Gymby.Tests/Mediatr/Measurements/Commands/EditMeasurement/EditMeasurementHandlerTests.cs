using AutoMapper;
using Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;
using Gymby.Application.Mediatr.Measurements.Commands.EditMeasurement;
using Gymby.Application.Mediatr.Measurements.Queries.GetMyMeasurements;
using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Measurements.Commands.EditMeasurement
{
    public class EditMeasurementHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public EditMeasurementHandlerTests()
        {
            MeasurementCommandTestFixture fixture = new MeasurementCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task EditMeasurementHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerGetMeasurement = new GetMyMeasurementsHandler(Context, Mapper, FileService);
            var handlerAddMeasurement = new AddMeasurementHandler(Context, Mapper, FileService);
            var handlerEditMeasurement = new EditMeasurementHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAddPhoto = new AddPhotoHandler(Context, Mapper, FileService);

            var expectedValue = 75.1;
            var UserCId = Guid.NewGuid();

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var appConfigOptionsMeasurement = Options.Create(new AppConfig());
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

            var result = await handlerAddMeasurement.Handle(new AddMeasurementCommand()
            {
                Type = MeasurementType.Shoulders,
                Unit = Units.Cm,
                Value = 30,
                UserId = UserCId.ToString()
            }, CancellationToken.None);

            var measurementId = await handlerGetMeasurement.Handle(new GetMyMeasurementsQuery(appConfigOptionsMeasurement)
            {
                UserId = UserCId.ToString(),

            }, CancellationToken.None);

            var firstMeasurement = measurementId.Measurements[0];
            var id = firstMeasurement.Id;

            await handlerEditMeasurement.Handle(new EditMeasurementCommand()
            {
                Id = id,
                Type = MeasurementType.Neck,
                Unit = Units.Kg,
                Value = expectedValue,
                UserId = UserCId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(
               await Context.Measurements.SingleOrDefaultAsync(measurement =>
                    measurement.Id == id && 
                    measurement.Unit == Units.Kg &&
                    measurement.Value == expectedValue &&
                    measurement.Type == MeasurementType.Neck &&
                    measurement.UserId == UserCId.ToString()));
            result.ShouldBeOfType<MeasurementsList>();
        }
    }
}
