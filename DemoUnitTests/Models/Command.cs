namespace DemoUnitTests.API.Models
{
    public class Command
    {
        public class CommandLine
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public int Id { get; set; }
        public List<CommandLine> Lines { get; set; } = [];


        // Info liée au client...
    }
}
