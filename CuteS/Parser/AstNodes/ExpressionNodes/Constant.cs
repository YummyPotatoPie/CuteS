using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class Constant : Expression
    {
        public Token Value { get; private set; }

        public Constant(Token value, int line) : base(line) => Value = value;
    }
}