using Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;
using Gymby.Application.Mediatr.Measurements.Commands.EditMeasurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Measurements.Commands.EditMeasurement
{
    public class EditMeasurementCommandTests
    {
        [Fact]
        public void EditMeasurementCommand_ShouldBePropertiesAreSetCorrectly()
        {
            // Arrange
            var command = new EditMeasurementCommand();
            var expectedId = "measurementTest";
            var expectedUserId = "testUser";
            var expectedDate = DateTime.Now;
            var expectedType = MeasurementType.Weight;
            var expectedValue = 100.5;
            var expectedUnit = Units.Kg;
            var expectedOptions = new Mock<IOptions<AppConfig>>().Object;

            // Act
            command.Id = expectedId;
            command.UserId = expectedUserId;
            command.Date = expectedDate;
            command.Type = expectedType;
            command.Value = expectedValue;
            command.Unit = expectedUnit;
            command.Options = expectedOptions;

            // Assert
            Assert.Equal(expectedId, command.Id);
            Assert.Equal(expectedUserId, command.UserId);
            Assert.Equal(expectedDate, command.Date);
            Assert.Equal(expectedType, command.Type);
            Assert.Equal(expectedValue, command.Value);
            Assert.Equal(expectedUnit, command.Unit);
            Assert.Equal(expectedOptions, command.Options);
        }

        [Fact]
        public void EditMeasurementCommand_ShouldBeDefaultUserIdIsNull()
        {
            // Arrange
            var command = new EditMeasurementCommand();

            // Assert
            Assert.Null(command.Id);
            Assert.Null(command.UserId);
        }
    }
}
