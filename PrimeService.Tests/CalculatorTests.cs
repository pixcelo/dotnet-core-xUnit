using Xunit;

namespace PrimeService.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void Add_ShouldReturnCorrectSum()
        {
            // Arrange
            int a = 5;
            int b = 3;

            // Act
            int result = _calculator.Add(a, b);

            // Assert
            Assert.Equal(8, result);
        }

        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(10, 5, 5)]
        [InlineData(0, 0, 0)]
        public void Subtract_ShouldReturnCorrectDifference(int a, int b, int expected)
        {
            // Act
            int result = _calculator.Subtract(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_ByZero_ShouldThrowDivideByZeroException()
        {
            // Arrange
            int a = 10;
            int b = 0;

            // Act & Assert
            Assert.Throws<System.DivideByZeroException>(() => _calculator.Divide(a, b));
        }
    }
}
