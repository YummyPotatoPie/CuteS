namespace Lexer.SymbolStreams
{
    /// <summary>
    /// Abstract class that containing required properties
    /// </summary>
    public abstract class AbstractSymbolStream : ISymbolStream
    {
        /// <summary>
        /// Position of current symbol at the current line. 
        /// </summary>
        protected int Position = 0;

        public abstract char PeekSymbol();

        public abstract char NextSymbol();

        public abstract bool EndOfStream();
    }
}
