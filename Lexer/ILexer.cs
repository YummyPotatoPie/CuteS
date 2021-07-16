using Lexer.Tokens;

namespace Lexer
{
    /// <summary>
    /// The interface must be implemented for all lexers
    /// </summary>
    public interface ILexer
    {
        /// <summary>
        /// Method returning token representation of next lexeme in a symbol stream
        /// </summary>
        /// <returns>Token of the next lexeme in a symbol stream</returns>
        public Token GetNextToken();
    }
}
