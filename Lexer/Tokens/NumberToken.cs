namespace Lexer.Tokens
{
    /// <summary>
    /// Class representing numeric token
    /// </summary>
    public class NumberToken : Token
    {
        /// <summary>
        /// Represents integer number value
        /// </summary>
        public readonly int Value;

        /// <summary>
        /// Construct number token
        /// </summary>
        /// <param name="attribute">Attribute of token</param>
        /// <param name="value">Numeric value of the token</param>
        public NumberToken(int value) : base(TokenAttribute.NUM) => Value = value;

        public override string ToString() => Value.ToString();
    }
}
