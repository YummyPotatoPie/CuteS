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

        public override string Emit() 
        {
            if (OperatorType.Tag == TokenAttributes.DotOp) return $"{Left.Emit()}{OperatorType.Lexeme}{Right.Emit()}";
            return $"{Left.Emit()} {OperatorType.Lexeme} {Right.Emit()}";
        }

        public override string ToString() => $"BinOp(Left({Left});Op({OperatorType});Right({Right}););";
    }
}