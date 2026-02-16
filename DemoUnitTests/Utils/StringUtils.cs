namespace DemoUnitTests.API.Utils
{
    public class StringUtils
    {
        public static string ToTitle(string value)
        {
            var words = value.Trim().Split(" ");
            words = words
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => w[0..1].ToUpper() + w[1..].ToLower())
                .ToArray();

            return string.Join(" ", words);
        }
    }
}
