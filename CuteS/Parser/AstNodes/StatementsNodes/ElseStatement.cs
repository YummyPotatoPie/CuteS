using System.Text;

namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class ElseStatement : Statement
    {
        public Statement[] Statements { get; private set; }

        public ElseStatement(Statement[] statements, int line) : base(line) => Statements = statements;

        public override string Emit()
        {
            StringBuilder statements = new();

            foreach (Statement statement in Statements) statements.Append(statement.Emit()).Append('\n');

            if (Statements.Length == 1 && Statements[0] is IfStatement) return $"else {Statements[0].Emit()}";
            else return $"else\n{{\n{statements}}}\n";
        }

        public override string ToString()
        {
            StringBuilder statements = new();

            foreach (Statement statement in Statements) statements.Append(statement.ToString());

            return $"Else(Statements({statements}););";
        }
    }
}