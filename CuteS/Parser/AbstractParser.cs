using CuteS.LexicalAnalyser;
using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.Parser
{
    public abstract class AbstractParser<T>
    {
        protected Lexer Lex;

        protected Token CurrentToken;

        public AbstractParser(Lexer lexer)
        {
            Lex = lexer;
            CurrentToken = Lex.NextToken();
        }

        protected void Match(int tokenTag)
        {
            if (CurrentToken == null) throw new SyntaxError();

            if (tokenTag == CurrentToken.Tag)
            {
                CurrentToken = Lex.NextToken();
            }
            else throw new SyntaxError();
        }

        public abstract T Parse();

    }
}