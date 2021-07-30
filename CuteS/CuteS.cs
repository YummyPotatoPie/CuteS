namespace CuteS
{
    public class CuteS
    {
        public static void Main(string[] args)
        {
            CompilerSettings argsParser = new ArgumentsParser(string.Join("", args)).Parse();
        }
    }
}
