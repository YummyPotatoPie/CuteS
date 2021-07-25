namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class IdentifierNode
    {
        public string Identifier { get; private set; }

        public IdentifierNode(string identifier) => Identifier = identifier;
    }
}
