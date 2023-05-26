using Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Measurements.Commands.AddMeasurement
{
    public class AddMeasurementCommandTests
    {
        [Fact]
        public void AddMeasurementCommand_ShouldBePropertiesAreSetCorrectly()
        {
            // Arrange
            var command = new AddMeasurementCommand();
            var expectedUserId = "testUser";
            var expectedDate = DateTime.Now;
            var expectedType = MeasurementType.Weight;
            var expectedValue = 100.5;
            var expectedUnit = Units.Kg;
            var expectedOptions = new Mock<IOptions<AppConfig>>().Object;

            // Act
            command.UserId = expectedUserId;
            command.Date = expectedDate;
            command.Type = expectedType;
            command.Value = expectedValue;
            command.Unit = expectedUnit;
            command.Options = expectedOptions;

            // Assert
            Assert.Equal(expectedUserId, command.UserId);
            Assert.Equal(expectedDate, command.Date);
            Assert.Equal(expectedType, command.Type);
            Assert.Equal(expectedValue, command.Value);
            Assert.Equal(expectedUnit, command.Unit);
            Assert.Equal(expectedOptions, command.Options);
        }

        [Fact]
        public void AddMeasurementCommand_ShouldBeDefaultUserIdIsNull()
        {
            // Arrange
            var command = new AddMeasurementCommand();

            // Assert
            Assert.Null(command.UserId);
        }
    }
}
