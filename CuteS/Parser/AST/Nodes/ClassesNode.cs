namespace CuteS.Parser.AST.Nodes
{
    public class ClassesNode : Node
    {
        public ClassNode[] Classes { get; private set; }

        public ClassesNode(ClassNode[] classes, int line) : base(line) => Classes = classes;

        public override string Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}