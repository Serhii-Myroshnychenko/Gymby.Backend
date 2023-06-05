using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Utils
{
    public class DateHandlerTests
    {
        [Fact]
        public void DateHandlerTests_GetNameOfDay_ReturnsCorrectNameOfDay()
        {
            // Arrange
            DateTime date = new DateTime(2023, 6, 5);

            // Act
            string result = DateHandler.GetNameOfDay(date);

            // Assert
            Assert.Equal("Monday", result);
        }

        [Fact]
        public void DateHandlerTests_GetNameOfDay_ReturnsFirstLetterUppercase()
        {
            // Arrange
            DateTime date = new DateTime(2023, 6, 5);

            // Act
            string result = DateHandler.GetNameOfDay(date);

            // Assert
            Assert.Equal('M', result[0]);
        }
    }
}
