using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class BinaryOperator : OperatorNode
    {
        public Expression Left { get; private set; }

        public Expression Right { get; private set; }

        public BinaryOperator(Expression left, Expression right, WordToken operatorType, int line) : base(operatorType, line) 
        {
            Left = left;
            Right = right;
        }
    }
}