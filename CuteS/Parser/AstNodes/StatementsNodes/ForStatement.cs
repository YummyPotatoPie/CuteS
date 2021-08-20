using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class ForStatement : Statement
    {
        // Expression or Declaration
        public Node IteratorExpression { get; private set; }

        public Expression ConditionExpression { get; private set; }

        public Expression CycleExpression { get; private set; }

        public Statement[] Statements { get; private set; }

        public ForStatement(Node iteratorExpression, Expression conditionExpression, Expression cycleExpression, Statement[] statements, int line) : base(line)
        {
            IteratorExpression = iteratorExpression;
            ConditionExpression = conditionExpression;
            CycleExpression = cycleExpression;
            Statements = statements;
        }
    }
}