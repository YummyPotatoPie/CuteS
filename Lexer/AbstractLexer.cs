using Lexer.Tokens;
using Lexer.SymbolStreams;

namespace Lexer
{
    /// <summary>
    /// Class of the abstract lexer containing special fields
    /// </summary>
    public abstract class AbstractLexer : ILexer
    {
        /// <summary>
        /// Symbol stream implementing ISymbolStream interface
        /// </summary>
        protected ISymbolStream SymbolStream;

        /// <summary>
        /// Current line 
        /// </summary>
        protected int Line = 1;

        /// <summary>
        /// Current symbol position at the current line
        /// </summary>
        protected int SymbolPosition = 1;

        /// <summary>
        /// Base AbstractLexer class constructor
        /// </summary>
        /// <param name="symbolStream">Any symbol stream that implements ISymbolStream interface</param>
        public AbstractLexer(ISymbolStream symbolStream) => SymbolStream = symbolStream;

        public abstract Token GetNextToken();
    }
}
