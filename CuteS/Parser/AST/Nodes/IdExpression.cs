namespace CuteS.Parser.AST.Nodes
{
    public class IdExpression : Node
    {
        public IdentifierNode[] Identifiers { get; private set; }
        public IdExpression(IdentifierNode[] identifiers, int line) : base(line) => Identifiers = identifiers;

        public override string Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}