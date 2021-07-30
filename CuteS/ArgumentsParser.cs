namespace CuteS
{
    public class ArgumentsParser
    {
        private readonly string _arguments;

        public ArgumentsParser(string arguments) => _arguments = arguments;

        public CompilerSettings Parse() => new();
    }
}
