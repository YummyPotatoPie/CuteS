namespace CuteS.LexicalAnalyser.Tokens
{
    public class WordToken : Token
    {
        public string Lexeme { get; private set; }

        public WordToken(string lexeme) : base(TokenAttributes.Identifier) => Lexeme = lexeme;
    }
}
