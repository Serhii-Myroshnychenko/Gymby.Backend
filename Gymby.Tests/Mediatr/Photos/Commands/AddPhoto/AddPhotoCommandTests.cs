using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Photos.Commands.AddPhoto
{
    public class AddPhotoCommandTests
    {
        [Fact]
        public void AddPhotoCommand_ShouldBeInitializesProperties()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var type = "Profile";

            // Act
            var command = new AddPhotoCommand(options, type);

            // Assert
            Assert.Null(command.UserId);
            Assert.Null(command.Photo);
            Assert.Equal(type, command.Type);
            Assert.Null(command.MeasurementDate);
            Assert.Same(options, command.Options);
        }

        [Fact]
        public void AddPhotoCommand_AssignsPropertyValues()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var type = "Profile";
            var userId = "testUserId";
            var photo = new Mock<IFormFile>().Object;
            var measurementDate = DateTime.Now;

            // Act
            var command = new AddPhotoCommand(options, type)
            {
                UserId = userId,
                Photo = photo,
                MeasurementDate = measurementDate
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(photo, command.Photo);
            Assert.Equal(measurementDate, command.MeasurementDate);
        }
    }
}
