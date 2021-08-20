namespace CuteS.SyntaxAnalyser.AstNodes
{
    public class Program : Node 
    {
        public Import[] Imports { get; private set; }

        public Namespace NamespaceNode { get; private set; }

        public Program(Import[] imports, Namespace namespaceNode, int line) : base(line) 
        {
            Imports = imports;
            NamespaceNode = namespaceNode;
        }
    }
}