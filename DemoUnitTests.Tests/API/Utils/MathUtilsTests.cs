using DemoUnitTests.Utils;

namespace DemoUnitTests.Tests.API.Utils
{
    public class MathUtilsTests
    {
        [Fact]
        public void RoundTo5Cents_with0() 
        {
            // arrange
            decimal value = 0;
            // act
            decimal result = MathUtils.RoundTo5Cents(value);
            // assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(14.1)]
        [InlineData(42)]
        [InlineData(15.05)]
        public void RoundTo5Cents_withNoRound(decimal value)
        {
            decimal result = MathUtils.RoundTo5Cents(value);
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(14.19, 14.2)]
        [InlineData(42.03, 42.05)]
        [InlineData(75.778, 75.8)]
        public void RoundTo5Cents_withCeil(decimal value, decimal expected)
        {
            decimal result = MathUtils.RoundTo5Cents(value);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(14.16, 14.15)]
        [InlineData(42.01, 42)]
        [InlineData(42.072, 42.05)]
        public void RoundTo5Cents_withFloor(decimal value, decimal expected)
        {
            decimal result = MathUtils.RoundTo5Cents(value);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void RoundTo5Cents_withNegative()
        {
            decimal value = -42.03m;
            // assert
            Assert.Throws<ArgumentException>(() =>
            {
                // act
                MathUtils.RoundTo5Cents(value);
            });
            
        }
    }
}
