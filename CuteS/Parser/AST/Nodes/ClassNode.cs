namespace CuteS.Parser.AST.Nodes
{
    public class ClassNode : Node
    {
        public IdentifierNode Identifier { get; private set; } 

        public ClassNode(IdentifierNode identifier, int line) : base(line) => Identifier = identifier;

        public override string Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}