namespace CuteS.Parser.AST.Nodes
{
    public class ProgramNode : Node
    {
        public ImportNode[] Imports { get; private set; }

        public NamespaceNode Namespace { get; private set; }
        
        public ProgramNode(ImportNode[] imports, NamespaceNode namespaceNode, int line) : base(line) 
        {
            Imports = imports;
            Namespace = namespaceNode;
        }

        public override string Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}