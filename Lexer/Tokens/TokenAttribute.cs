namespace Lexer.Tokens
{
    /// <summary>
    /// Class representing attributes of token
    /// </summary>
    public static class TokenAttribute
    {
        /// <summary>
        /// Attribute of the token
        /// </summary>
        public readonly static int
            NUM     = 256, REAL     = 257, TRUE     = 258, FALSE    = 259, ID       = 260,
            IF      = 261, ELSE     = 262, WHILE    = 263, BREAK    = 264, INDEX    = 265,
            EQ      = 266, GE       = 267, LE       = 268, NE       = 269, OR       = 270, 
            AND     = 272, MINUS    = 273;

    }
}
