using Gymby.Application.Mediatr.Measurements.Commands.DeleteMeasurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Measurements.Commands.DeleteMeasurement
{
    public class DeleteMeasurementCommandTests
    {
        [Fact]
        public void DeletePhotoCommand_WithValidOptions_ShouldSetOptionsProperty()
        {
            // Arrange
            var options = new Mock<IOptions<AppConfig>>();

            // Act
            var command = new DeleteMeasurementCommand(options.Object);

            // Assert
            Assert.Same(options.Object, command.Options);
        }

        [Fact]
        public void DeletePhotoCommand_ShouldBeSettableProperties()
        {
            // Arrange
            var options = new Mock<IOptions<AppConfig>>();
            var userId = "user123";
            var id = "measurement123";

            // Act
            var command = new DeleteMeasurementCommand(options.Object)
            {
                UserId = userId,
                Id = id
            };

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(id, command.Id);
        }
    }
}
