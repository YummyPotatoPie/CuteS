using CuteS.LexicalAnalyser.Tokens;

namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class Identifier : Expression
    {
        public WordToken IdentifierName { get; private set; }

        public Identifier(WordToken identifierName, int line) : base(line) => IdentifierName = identifierName;
    }
}