using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class ExpressionStatement : Statement
    {
        public Expression ExpressionStmt { get; private set; }

        public ExpressionStatement(Expression expression, int line) : base(line) => ExpressionStmt = expression;

        public override string Emit() => $"{ExpressionStmt.Emit()};";

        public override string ToString() => $"ExpressionStatement({ExpressionStmt});";
    }
}