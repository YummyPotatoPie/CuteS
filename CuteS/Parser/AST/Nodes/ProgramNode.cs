namespace CuteS.Parser.AST.Nodes
{
    public class ProgramNode : Node
    {
        public ImportNode[] Imports { get; private set; }
        
        public ProgramNode(ImportNode[] imports, int line) : base(line) => Imports = imports;

        public override string Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}