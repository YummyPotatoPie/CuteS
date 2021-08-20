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
    }
}