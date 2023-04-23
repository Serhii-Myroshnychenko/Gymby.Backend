namespace Gymby.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void MyTestMethod_Success()
        {
            // Arrange
            var expected = 4;
            var a = 2;
            var b = 2;

            // Act
            var result = a + b;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MyTestMethod_Failed()
        {
            // Arrange
            var expected = 4;
            var a = 2;
            var b = 1;

            // Act
            var result = a + b;

            // Assert
            Assert.NotEqual(expected, result);
        }
    }
}