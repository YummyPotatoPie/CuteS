namespace CuteS.SyntaxAnalyser.AstNodes.ExpressionNodes
{
    public class Expression : Node 
    {
        public Expression ExpressionRoot { get; private set; }

        public Expression(int line) : base(line) { }
    }
}