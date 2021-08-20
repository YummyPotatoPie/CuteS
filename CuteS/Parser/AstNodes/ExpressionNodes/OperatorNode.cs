using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class OperatorNode : Expression
    {
        public WordToken OperatorType { get; private set; }

        public OperatorNode(WordToken operatorType, int line) : base(line) => OperatorType = operatorType;
    }
}