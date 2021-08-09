namespace CuteS.Parser.AST.Nodes
{
    public abstract class Node
    {
        public int Line { get; private set; }

        public abstract string Emit();

        public Node(int line) => Line = line;
    }
}