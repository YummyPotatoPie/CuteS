using System.Text;

using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class IfStatement : Statement
    {
        public Expression Condition { get; private set; }

        public Statement[] Statements { get; private set; }

        public ElseStatement Else { get; private set; }

        public IfStatement(Expression condition, Statement[] statements, ElseStatement elseStatement, int line) : base(line)
        {
            Condition = condition;
            Statements = statements;
            Else = elseStatement;
        }

        public override string Emit()
        {
            StringBuilder statements = new();

            foreach (Statement statement in Statements) statements.Append(statement.Emit()).Append('\n');

            string elseEmit = Else == null ? "" : Else.Emit();

            return $"if ({Condition.Emit()})\n{{\n{statements}}}\n{elseEmit}";
        }

        public override string ToString()
        {
            StringBuilder statements = new();

            foreach (Statement statement in Statements) statements.Append(statement.ToString());
            string elseStmt = Else == null ? "Else(null);" : Else.ToString();

            return $"If(Condition({Condition});Statements({statements});{elseStmt});";
        }
    }
}