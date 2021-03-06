namespace CuteS.SyntaxAnalyser.AstNodes
{
    public abstract class Node 
    {
        public int Line { get; private set; }

        public Node(int line) => Line = line;

        public abstract string Emit();

        public override string ToString() => Line.ToString();
    }
}