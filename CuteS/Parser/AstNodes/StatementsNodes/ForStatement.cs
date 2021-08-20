using System.Text;

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

        public override string Emit()
        {
            StringBuilder statements = new();

            foreach (Statement statement in Statements) statements.Append(statement.Emit()).Append('\n');


            string iterator = IteratorExpression == null ? "" : IteratorExpression.Emit();
            string condition = ConditionExpression == null ? "" : ConditionExpression.Emit();
            string cycle = CycleExpression == null ? "" : CycleExpression.Emit();

            string declareSymbol = "";
            if (IteratorExpression == null) declareSymbol = ";";
            else if (IteratorExpression is Declaration)declareSymbol = "";

            return $"\nfor ({iterator}{declareSymbol} {condition}; {cycle})\n{{\n{statements}}}";
        }

        public override string ToString()
        {
            StringBuilder statements = new();

            foreach (Statement statement in Statements) statements.Append(statement.ToString());

            return $"For(Iterator({IteratorExpression})Condition({ConditionExpression});Cycle({CycleExpression});Statements({statements}););";
        }
    }
}