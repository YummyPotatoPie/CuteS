using CuteS.Lexer.Tokens;
using SymbolStreams;
using System.Collections;
using System.Text;

namespace CuteS.Lexer
{
    public class LexAnalyser
    {
        private readonly ISymbolStream<char> _symbolStream;

        public Hashtable Table { get; private set; } = new();

        public int Line { get; private set; } = 1;

        public LexAnalyser(ISymbolStream<char> symbolStream)
        {
            _symbolStream = symbolStream;
            Reserve(WordToken.Using);
            Reserve(WordToken.Namespace);
            Reserve(WordToken.Function);
            Reserve(WordToken.Class);
            Reserve(WordToken.Bool);
            Reserve(WordToken.Float);
            Reserve(WordToken.Integer);
            Reserve(WordToken.Char);
        }
        private void Reserve(WordToken token) => Table.Add(token.Lexeme, token);

        private Token ReadWord()
        {
            StringBuilder word = new();
            char buffer = _symbolStream.Peek();
            while (char.IsLetterOrDigit(buffer) || buffer == '_')
            {
                word.Append(buffer);
                buffer = _symbolStream.Next();
            }

            string stringWord = word.ToString();
            WordToken token = (WordToken)Table[stringWord];
            if (token != null) return token;

            token = new WordToken((int)Attribute.Identificator, stringWord);
            return token;
        }

        private void SkipWhiteSpaces()
        {
            char currentSymbol = _symbolStream.Peek();
            while (char.IsWhiteSpace(currentSymbol) && currentSymbol != default)
            {
                if (currentSymbol == '\n') Line++;
                currentSymbol = _symbolStream.Next();
            }
        }

        public Token NextToken()
        {
            SkipWhiteSpaces();
            char currentSymbol = _symbolStream.Peek();
            if (currentSymbol == default) return null;
            if (char.IsLetter(currentSymbol) || currentSymbol == '_') return ReadWord();
            _symbolStream.Next();
            return new Token(currentSymbol);
        }
    }
}
