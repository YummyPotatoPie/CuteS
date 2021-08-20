using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class UnaryOperator : OperatorNode
    {
        public Expression ExpressionNode { get; private set; }

        public UnaryOperator(Expression expression, WordToken operatorType, int line) : base(operatorType, line) => ExpressionNode = expression;
    }
}