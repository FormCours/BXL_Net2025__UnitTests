namespace DemoUnitTests.Utils
{
    public class MathUtils
    {

        public static decimal RoundTo5Cents(decimal value)
        {
            if(value < 0)
            {
                throw new ArgumentException("The value must be greater than 0");
            }
            return Math.Round(value * 20) / 20;
        }
    }
}
