namespace CuteS.SyntaxAnalyser.AstNodes.StatementsNodes
{
    public class Statement : Node 
    {
        public Statement(int line) : base(line) { }

        public override string Emit() => Line.ToString();
    }
}