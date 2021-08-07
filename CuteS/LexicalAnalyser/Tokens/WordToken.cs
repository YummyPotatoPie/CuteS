namespace CuteS.LexicalAnalyser.Tokens
{
    public class WordToken : Token
    {
        public string Lexeme { get; private set; }

        // Reserved words 
        public readonly static WordToken
            Namespace = new("namespace", TokenAttributes.Namespace),
            Class = new("class", TokenAttributes.Class),
            Import = new("import", TokenAttributes.Import);

        /// <summary>
        /// WordToken base constructor
        /// </summary>
        /// <param name="lexeme">Lexeme that represents token</param>
        /// <param name="tag">Default value is 256 that equal TokenAttribute.Identifier</param>
        public WordToken(string lexeme, int tag = 256) : base(tag) => Lexeme = lexeme;
    }
}