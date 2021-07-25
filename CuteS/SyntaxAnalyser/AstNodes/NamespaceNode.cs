namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class NamespaceNode
    {
        public IdentifierNode NamespaceName { get; private set; }

        public ClassesNode Classes { get; private set; }

        public NamespaceNode(IdentifierNode identifierNode, ClassesNode classesNode)
        {
            NamespaceName = identifierNode;
            Classes = classesNode;
        }
    }
}
