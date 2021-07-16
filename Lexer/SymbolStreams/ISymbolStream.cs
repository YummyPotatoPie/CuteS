namespace Lexer.SymbolStreams
{
    /// <summary>
    /// Interface for symbols streams
    /// </summary>
    public interface ISymbolStream
    {
        /// <summary>
        /// Returns current symbol at the stream
        /// </summary>
        /// <returns>Current stream symbol</returns>
        public char PeekSymbol();

        /// <summary>
        /// Returns next symbol at the stream
        /// </summary>
        /// <returns>Next stream symbol. If stream reached the end of file returns NUL (0)</returns>
        public char NextSymbol();

        /// <summary>
        /// Checks if the stream has reached the end of the file
        /// </summary>
        /// <returns>Returns true if stream reaches the end of file, otherwise returns false</returns>
        public bool EndOfStream();
    }
}
