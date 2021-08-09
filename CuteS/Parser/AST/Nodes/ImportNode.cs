namespace CuteS.Parser.AST.Nodes
{
    public class ImportNode : Node
    {
        public IdExpression NamespaceId { get; private set; } 

        public ImportNode(IdExpression namespaceId, int line) : base(line) => NamespaceId = namespaceId;

        public override string Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}