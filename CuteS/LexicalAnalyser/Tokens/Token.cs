namespace CuteS.LexicalAnalyser.Tokens
{
    public class Token
    {
        public int Tag { get; private set; }

        public Token(int tag) => Tag = tag;
    }
}
