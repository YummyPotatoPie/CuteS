using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class ReturnStatement : Statement
    {
        public Expression ReturnValue { get; private set; }

        public ReturnStatement(Expression returnValue, int line) : base(line) => ReturnValue = returnValue;
    }
}