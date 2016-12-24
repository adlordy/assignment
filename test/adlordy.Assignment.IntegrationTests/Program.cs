namespace adlordy.Assignment.IntergrationTests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            using (var program = new Xunit.Runner.DotNet.Program())
                return program.Run(args);
        }
    }
}
