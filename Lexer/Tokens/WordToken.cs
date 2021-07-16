namespace Lexer.Tokens
{
    /// <summary>
    /// Class representing word token
    /// </summary>
    public class WordToken : Token
    {
        /// <summary>
        /// String representing lexeme of token
        /// </summary>
        public readonly string Lexeme;

        /// <summary>
        /// Reserved identificators
        /// </summary>
        public readonly static WordToken
            AND     = new(TokenAttribute.AND, "&&"),
            OR      = new(TokenAttribute.OR, "||"),
            EQ      = new(TokenAttribute.EQ, "=="),
            NE      = new(TokenAttribute.NE, "!="),
            LE      = new(TokenAttribute.LE, "<="),
            GE      = new(TokenAttribute.GE, ">="),
            MINUS   = new(TokenAttribute.MINUS, "-"),
            TRUE    = new(TokenAttribute.TRUE, "True"),
            FALSE   = new(TokenAttribute.FALSE, "False");


        /// <summary>
        /// Construct word token
        /// </summary>
        /// <param name="attribute">Attribute of token</param>
        /// <param name="lexeme">String representing lexeme</param>
        public WordToken(int attribute, string lexeme) : base(attribute) => Lexeme = lexeme;

        public override string ToString() => Lexeme;
    }
}
