namespace Lexer.SymbolStreams
{
    /// <summary>
    /// A character stream that loads all information from a file at once
    /// </summary>
    public class LoadedSymbolStream : AbstractSymbolStream
    {
        /// <summary>
        /// All data from file
        /// </summary>
        private readonly string _loadedStream;

        /// <summary>
        /// Constructor accepting the entire data stream from a file
        /// </summary>
        /// <param name="loadedStream">All data from input file</param>
        public LoadedSymbolStream(string loadedStream) => _loadedStream = loadedStream;

        public override char PeekSymbol() => _loadedStream[Position];

        public override char NextSymbol() => EndOfStream() ? _loadedStream[++Position] : (char)0;

        public override bool EndOfStream() => Position == _loadedStream.Length;
    }
}
