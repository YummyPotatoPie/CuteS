namespace CuteS.LexicalAnalyser.Tokens
{
    public class LiteralToken : Token
    {
        public string Literal { get; private set; }

        public LiteralToken(string literal) : base(TokenAttributes.String) => Literal = literal;
    }
}
