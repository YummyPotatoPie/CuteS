namespace Lexer.Tokens
{
    /// <summary>
    /// Class representing abstract lexeme token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Attribute of token
        /// </summary>
        public readonly int Attribute;

        /// <summary>
        /// Construct abstract token
        /// </summary>
        /// <param name="attribute">Attribute of token</param>
        public Token(int attribute) => Attribute = attribute;

        public override string ToString() => "" + (char)Attribute;
    }
}
