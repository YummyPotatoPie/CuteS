namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class ElseStatement : Statement
    {
        public Statement[] Statements { get; private set; }

        public ElseStatement(Statement[] statements, int line) : base(line) => Statements = statements;
    }
}