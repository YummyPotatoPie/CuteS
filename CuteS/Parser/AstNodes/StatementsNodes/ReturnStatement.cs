using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class ReturnStatement : Statement
    {
        public Expression ReturnValue { get; private set; }

        public ReturnStatement(Expression returnValue, int line) : base(line) => ReturnValue = returnValue;

        public override string Emit() => ReturnValue == null ? "return;" : $"return {ReturnValue.Emit()};";

        public override string ToString() => ReturnValue == null ? "Return(null);" : $"ReturnValue({ReturnValue});";
    }
}