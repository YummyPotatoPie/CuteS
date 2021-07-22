using CuteS.Lexer.Tokens;
using SymbolStreams;
using System.Collections;

namespace CuteS.Lexer
{
    public class Lexer
    {
        private ISymbolStream<char> _symbolStream;

        public Hashtable Table { get; private set; } = new();

        public Lexer(ISymbolStream<char> symbolStream) => _symbolStream = symbolStream;

        public Token NextToken() => new();
    }
}
