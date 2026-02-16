namespace DemoUnitTests.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // AAA

            // ARRANGE
            // définition du context
            int nbJoueurs = 42;

            // ACT
            // ce que je vais tester
            nbJoueurs += 1;

            // ASSERT
            // ce que je m'attends à avoir comme résultat
            Assert.Equal(43, nbJoueurs);
        }
    }
}
