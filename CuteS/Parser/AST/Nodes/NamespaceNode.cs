namespace CuteS.Parser.AST.Nodes
{
    public class NamespaceNode : Node
    {
        public IdExpression NamespaceName { get; private set; }

        public ClassesNode Classes { get; private set; } 

        public NamespaceNode(IdExpression namespaceName, int line) : base(line) => NamespaceName = namespaceName;

        public override string Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}