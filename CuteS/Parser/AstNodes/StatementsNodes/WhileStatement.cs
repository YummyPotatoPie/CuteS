using System.Text;

using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class WhileStatement : Statement
    {
        public Expression Condition { get; private set; }

        public Statement[] Statements { get; private set; } 

        public WhileStatement(Expression condition, Statement[] statements, int line) : base(line)
        {
            Condition = condition;
            Statements = statements;
        }

        public override string Emit()
        {
            StringBuilder statements = new();

            foreach (Statement statement in Statements) statements.Append(statement.Emit()).Append('\n');

            return $"\nwhile ({Condition.Emit()})\n{{\n{statements}}}";
        }

        public override string ToString()
        {
            StringBuilder statements = new();

            foreach (Statement statement in Statements) statements.Append(statement.ToString());

            return $"While(Condition({Condition});Statements({statements}););";
        }
    }
}