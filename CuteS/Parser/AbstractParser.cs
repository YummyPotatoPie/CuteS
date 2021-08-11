using CuteS.LexicalAnalyser;

using CuteS.Parser.AstNodes;

namespace CuteS.Parser
{
    public abstract class AbstractParser<T> where T : Node
    {
        protected Lexer Lex;

        public AbstractParser(ref Lexer lexer) => Lex = lexer;

        protected void Match(int tokenTag)
        {
            if (Lex.CurrentToken == null) throw new SyntaxError();

            if (tokenTag == Lex.CurrentToken.Tag) Lex.NextToken();
            else throw new SyntaxError();
        }

        public abstract T Parse();
    }
}