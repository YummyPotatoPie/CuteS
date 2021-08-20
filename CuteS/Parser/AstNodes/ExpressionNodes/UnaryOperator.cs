using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class UnaryOperator : OperatorNode
    {
        public Expression ExpressionNode { get; private set; }

        public UnaryOperator(Expression expression, WordToken operatorType, int line) : base(operatorType, line) => ExpressionNode = expression;

        public override string Emit() 
        {
            if (OperatorType.Tag == TokenAttributes.New) return $"{OperatorType.Lexeme} {ExpressionNode.Emit()}";
            return $"{OperatorType.Lexeme}{ExpressionNode.Emit()}";
        }

        public override string ToString() => $"UnaryOperator(OperatorType({OperatorType});Expression({ExpressionNode}););";
    }
}