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

    }
}