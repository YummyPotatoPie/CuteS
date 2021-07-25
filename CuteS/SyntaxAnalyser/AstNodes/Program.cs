namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Program
    {
        public UsingsNode UsingsNode { get; private set; }

        public NamespaceNode NamespaceNode { get; private set; }

        public Program(UsingsNode usingsNode, NamespaceNode namespaceNode)
        {
            UsingsNode = usingsNode;
            NamespaceNode = namespaceNode;
        }
    }
}
