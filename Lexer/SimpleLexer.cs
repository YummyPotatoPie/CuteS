using Lexer.SymbolStreams;
using Lexer.Tokens;
using Lexer.Exceptions;

namespace Lexer
{
    /// <summary>
    /// Simple lexer using not the fastest algorithms
    /// </summary>
    public class SimpleLexer : AbstractLexer
    {
        /// <summary>
        /// Base constructor of SimpleLexer class
        /// </summary>
        /// <param name="symbolStream"></param>
        public SimpleLexer(ISymbolStream symbolStream) : base(symbolStream) { }

        /// <summary>
        /// Skip comment line
        /// </summary>
        private void SkipComment()
        {
            while (SymbolStream.PeekSymbol() != '\n') SymbolStream.NextSymbol();
           
            Line++;
            SymbolPosition = 1;
            if (SymbolStream.NextSymbol() == '#') SkipComment();
        }

        /// <summary>
        /// Checks if symbol is a operator symbol
        /// </summary>
        /// <param name="symbol">Current symbol at symbol stream</param>
        /// <returns></returns>
        private bool IsOperatorSymbol(char symbol) => "!<>=-|&".IndexOf(symbol) > -1;
        
        /// <summary>
        /// Returns token representation of the operator
        /// </summary>
        /// <returns>Token representing operator</returns>
        private Token ReadOperator()
        {
            switch (SymbolStream.PeekSymbol())
            {
                case '&':
                    if (SymbolStream.NextSymbol() == '&') return WordToken.AND;
                    else return new Token('&');
                case '|':
                    if (SymbolStream.NextSymbol() == '|') return WordToken.OR;
                    else return new Token('|');
                case '=':
                    if (SymbolStream.NextSymbol() == '=') return WordToken.EQ;
                    else return new Token('=');
                case '!':
                    if (SymbolStream.NextSymbol() == '=') return WordToken.NE;
                    else return new Token('!');
                case '<':
                    if (SymbolStream.NextSymbol() == '=') return WordToken.LE;
                    else return new Token('<');
                case '>':
                    if (SymbolStream.NextSymbol() == '=') return WordToken.GE;
                    else return new Token('>');
                default:
                    throw new SyntaxError($"Expected operator: line {Line}, position {SymbolPosition}");
            }
        }

        /// <summary>
        /// Skips whitespaces
        /// </summary>
        private void SkipWhiteSpaces()
        {
            char currentSymbol = SymbolStream.PeekSymbol();
            while (char.IsWhiteSpace(currentSymbol))
            {
                if (currentSymbol == '\n')
                {
                    Line++;
                    SymbolPosition = 0;
                }
                currentSymbol = SymbolStream.NextSymbol();
                SymbolPosition++;
            }
        }

        public override Token GetNextToken()
        {
            SkipWhiteSpaces();
            if (SymbolStream.PeekSymbol() == '#') SkipComment();
            if (IsOperatorSymbol(SymbolStream.PeekSymbol())) return ReadOperator();
            throw new System.NotImplementedException();
        }
    }
}
