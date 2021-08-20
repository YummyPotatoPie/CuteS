namespace CuteS.LexicalAnalyser.Tokens
{
    public class Token
    {
        public int Tag { get; private set; }

        public Token(int tag) => Tag = tag;

        public override string ToString() => new string(new char[] { (char)Tag });
    }
}