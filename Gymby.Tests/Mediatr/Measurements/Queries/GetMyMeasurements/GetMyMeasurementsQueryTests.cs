using Gymby.Application.Mediatr.Measurements.Queries.GetMyMeasurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Measurements.Queries.GetMyMeasurements
{
    public class GetMyMeasurementsQueryTests
    {
        [Fact]
        public void GetMyMeasurementsQuery_ShouldBeSetOptions()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;

            // Act
            var result = new GetMyMeasurementsQuery(options);

            // Assert
            Assert.NotNull(result);
            Assert.Same(options, result.Options);
        }

        [Fact]
        public void GetMyMeasurementsQuery_ShouldBeSetProperties()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var UserId = "testUserId";

            // Act
            var result = new GetMyMeasurementsQuery(options)
            {
                UserId = UserId
            };

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserId, result.UserId);
        }
    }
}
