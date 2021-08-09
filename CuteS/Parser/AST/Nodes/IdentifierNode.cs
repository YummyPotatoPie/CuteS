namespace CuteS.Parser.AST.Nodes
{
    public class IdentifierNode : Node 
    {
        public string Identifier { get; private set; }

        public IdentifierNode(string identifier, int line) : base(line) => Identifier = identifier;

        public override string Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}