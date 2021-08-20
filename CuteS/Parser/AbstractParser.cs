using CuteS.LexicalAnalyser;

namespace CuteS.SyntaxAnalyser
{
    public abstract class AbstractParser<T> 
    {
        protected Lexer Lex;

        public AbstractParser(ref Lexer lex) => Lex = lex;

        public abstract T Parse();

        public void Match(int tag)
        {
            if (Lex.CurrentToken == null) throw new SyntaxError("Unexpected end of file", Lex.Line);

            if (Lex.CurrentToken.Tag == tag) Lex.NextToken();
            else throw new SyntaxError($"Unexpected token {Lex.CurrentToken}", Lex.Line);
        }
    }
}