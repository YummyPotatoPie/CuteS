using CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes;

namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class Declaration : Statement
    {
        public Type IdentifierType { get; private set; }

        public Expression AssignExpression { get; private set; }

        public Declaration(Type identifierType, Expression assignExpression, int line) : base(line)
        {
            IdentifierType = identifierType;
            AssignExpression = assignExpression;
        }
    }
}